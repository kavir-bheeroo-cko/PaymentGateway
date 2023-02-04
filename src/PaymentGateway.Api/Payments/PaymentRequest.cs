using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaymentGateway.Api.Payments
{
    public record PaymentRequest
    {
        [JsonPropertyName("card_number")]
        [Required(ErrorMessage = "card_number_missing")]
        [StringLength(19, MinimumLength = 14, ErrorMessage = "card_number_invalid")]
        public string CardNumber { get; init; }

        [JsonPropertyName("expiry_month")]
        [Required(ErrorMessage = "card_expiry_month_missing")]
        [Range(1, 12, ErrorMessage = "card_expiry_month_invalid")]
        public int ExpiryMonth { get; init; }

        [JsonPropertyName("expiry_year")]
        [Required(ErrorMessage = "card_expiry_year_missing")]
        [Range(2023, 9999, ErrorMessage = "card_expiry_year_invalid")]
        // todo: add custom validator
        public int ExpiryYear { get; init; }

        [JsonPropertyName("amount")]
        [Required(ErrorMessage = "amount_missing")]
        public uint Amount { get; init; }

        [JsonPropertyName("currency")]
        [Required(ErrorMessage = "currency_missing")]
        // todo: add custom validator
        public string Currency { get; init; }

        [JsonPropertyName("cvv")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "card_cvv_length_invalid")]
        public string Cvv { get; init; }
    }
}
