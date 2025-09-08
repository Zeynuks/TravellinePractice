using System.ComponentModel.DataAnnotations;

namespace CurrencyExchanger.Models.Currency
{
    // TODO: зачем модели
    public class GetPricesRequest
    {
        [Required]
        public string PaymentCurrency { get; set; } = string.Empty;

        [Required]
        public string PurchasedCurrency { get; set; } = string.Empty;

        [Required]
        public DateTime? FromDateTime { get; set; }

        public DateTime? ToDateTime { get; set; }
    }
}