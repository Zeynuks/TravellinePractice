using CurrencyExchanger.Entities;
using CurrencyExchanger.Models.Currency;

namespace CurrencyExchanger.Services
{
    public interface ICurrencyService
    {
        IEnumerable<Currency> GetAll();
        Currency GetByCode( string code );
        IEnumerable<PriceChange> GetPriceChanges( GetPricesRequest model );
    }
}