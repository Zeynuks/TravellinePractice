using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CurrencyExchanger.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchanger.Helpers
{
    public static class CurrencyHelper
    {
        public static void AddCurrencyData(WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                DataContext ctx = scope.ServiceProvider.GetRequiredService<DataContext>();
                if (!ctx.Currencies.Any())
                {
                    IEnumerable<Currency> currencies = LoadCurrenciesFromJson(app);
                    ctx.Currencies.AddRange(currencies);
                    ctx.SaveChanges();
                }
            }

            IEnumerable<DateTime> dates = EnumerateDailyUtcMidnightFromYearStartToToday();
            AddPrices(app, dates);
            _ = Task.Run(() => RunDailyAtUtc00Async(app));
        }

        private static async Task RunDailyAtUtc00Async(WebApplication app)
        {
            while (true)
            {
                TimeSpan delay = GetDelayUntilNextUtc00();
                await Task.Delay(delay);
                try
                {
                    DateTime todayUtc = Utc00Today();
                    AddPrices(app, new[] { todayUtc });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static TimeSpan GetDelayUntilNextUtc00()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime next = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 0, 0, 0, DateTimeKind.Utc);
            if (utcNow >= next)
            {
                next = next.AddDays(1);
            }
            return next - utcNow;
        }

        private static DateTime Utc00Today()
        {
            DateTime utcNow = DateTime.UtcNow;
            return new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 0, 0, 0, DateTimeKind.Utc);
        }

        private static IEnumerable<DateTime> EnumerateDailyUtcMidnightFromYearStartToToday()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime start = new DateTime(utcNow.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime end = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 0, 0, 0, DateTimeKind.Utc);
            for (DateTime d = start; d <= end; d = d.AddDays(1))
            {
                yield return d;
            }
        }

        private static IEnumerable<Currency> LoadCurrenciesFromJson(WebApplication app)
        {
            IConfiguration config = app.Services.GetRequiredService<IConfiguration>();
            string? path = config["CurrencyData:JsonPath"];
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "currencies.json";
            }
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(app.Environment.ContentRootPath, path);
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Currencies JSON not found", path);
            }

            string json = File.ReadAllText(path);
            List<Currency> currencies = JsonSerializer.Deserialize<List<Currency>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? [ ];

            foreach (Currency c in currencies)
            {
                if (string.IsNullOrWhiteSpace(c.Code))
                {
                    throw new InvalidDataException("Currency Code is required in JSON.");
                }
                if (c.MinPrice > c.MaxPrice)
                {
                    throw new InvalidDataException($"Currency {c.Code}: MinPrice must be <= MaxPrice");
                }
            }

            return currencies;
        }

        private static void AddPrices(WebApplication app, IEnumerable<DateTime> datesUtc)
        {
            using IServiceScope scope = app.Services.CreateScope();
            DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();

            List<Currency> currencies = context.Currencies.AsNoTracking().ToList();
            if (currencies.Count == 0)
            {
                return;
            }

            DateTime[] dateTimes = datesUtc as DateTime[] ?? datesUtc.ToArray();
            if (dateTimes.Length == 0)
            {
                return;
            }

            DateTime minDate = dateTimes.Min();
            DateTime maxDate = dateTimes.Max();

            var existing = context.CurrencyPrices
                .AsNoTracking()
                .Where(p => p.DateTime >= minDate && p.DateTime <= maxDate)
                .Select(p => new { p.CurrencyCode, p.DateTime })
                .ToHashSet();

            List<CurrencyPrice> batch = [ ];
            foreach (DateTime dateUtc in dateTimes)
            {
                DateTime normalized = new(dateUtc.Year, dateUtc.Month, dateUtc.Day, 0, 0, 0, DateTimeKind.Utc);

                foreach (Currency currency in currencies)
                {
                    var key = new { CurrencyCode = currency.Code, DateTime = normalized };
                    if (existing.Contains(key))
                    {
                        continue;
                    }

                    decimal price = GenerateDeterministicPrice(currency.Code, normalized, 1, 100);
                    if (price == 0m)
                    {
                        price = currency.MinPrice == 0m ? 1m : currency.MinPrice;
                    }

                    batch.Add(new CurrencyPrice
                    {
                        CurrencyCode = currency.Code,
                        Currency = null!,
                        DateTime = normalized,
                        Price = price
                    });
                }
            }

            if (batch.Count == 0)
            {
                return;
            }

            context.CurrencyPrices.AddRange(batch);
            context.SaveChanges();
        }

        private static decimal GenerateDeterministicPrice(string code, DateTime dateUtc, decimal min, decimal max)
        {
            if (min > max) (min, max) = (max, min);
            if (min == max) return min;

            string key = $"{code}:{dateUtc:yyyyMMdd}";
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(key));
            
            BigInteger big = new(bytes, isUnsigned: true, isBigEndian: true);
            ulong u64 = (ulong)(big % (BigInteger.One << 64));
            double u = u64 / (double)ulong.MaxValue;

            double price = (double)min + u * (double)(max - min);
            return Math.Round((decimal)price, 2, MidpointRounding.AwayFromZero);
        }

    }
}
