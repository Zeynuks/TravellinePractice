namespace CurrencyExchanger.Models.Currency
{
    public class PriceChange
    {
        public string PurchasedCurrencyCode { get; set; } = string.Empty;

        public string PaymentCurrencyCode { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTime DateTime { get; set; }
    }
}