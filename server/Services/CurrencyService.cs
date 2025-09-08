using CurrencyExchanger.Entities;
using CurrencyExchanger.Helpers;
using CurrencyExchanger.Models.Currency;

namespace CurrencyExchanger.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly DataContext _context;

        public CurrencyService( DataContext context )
        {
            _context = context;
        }

        public IEnumerable<Currency> GetAll()
        {
            return _context.Currencies;
        }

        public Currency GetByCode( string code )
        {
            Currency? currency = _context.Currencies.Find( code );
            if ( currency == null )
            {
                throw new KeyNotFoundException( "Currency not found" );
            }

            return currency;
        }

        // TODO
        public IEnumerable<PriceChange> GetPriceChanges( GetPricesRequest model )
        {
            string purchasedCurrency = model.PurchasedCurrency;
            string paymentCurrency = model.PaymentCurrency;

            IQueryable<string> currencyCodes = _context.Currencies.Select( c => c.Code );
            if ( !currencyCodes.Contains( purchasedCurrency ) )
            {
                throw new AppException( $"Unknown currency {purchasedCurrency}" );
            }

            if ( !currencyCodes.Contains( paymentCurrency ) )
            {
                throw new AppException( $"Unknown currency {paymentCurrency}" );
            }

            IEnumerable<PriceChange> result = _context.CurrencyPrices
                .Where( c => ( c.CurrencyCode == purchasedCurrency || c.CurrencyCode == paymentCurrency ) &&
                             c.DateTime >= model.FromDateTime &&
                             ( model.ToDateTime == null || c.DateTime <= model.ToDateTime ) )
                .OrderBy( c => c.DateTime )
                .ToList()
                .GroupBy( c => c.DateTime )
                .Select( g =>
                {
                    CurrencyPrice? purchased = g.FirstOrDefault( item => item.CurrencyCode == purchasedCurrency );
                    CurrencyPrice? payment = g.FirstOrDefault( item => item.CurrencyCode == paymentCurrency );

                    if ( purchased == null || payment == null )
                    {
                        throw new AppException(
                            $"Grouping should contain both currencies, there are no currencies payment = {payment}, purchased = {purchased} for date {g.Key}" );
                    }

                    return new PriceChange
                    {
                        DateTime = g.Key,
                        PaymentCurrencyCode = payment.CurrencyCode,
                        PurchasedCurrencyCode = purchased.CurrencyCode,
                        Price = decimal.Round( purchased.Price / payment.Price, 3, MidpointRounding.ToPositiveInfinity )
                    };
                } );

            return result;
        }
    }
}