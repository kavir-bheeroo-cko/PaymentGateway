using System.Text.Json.Serialization;

namespace PaymentGateway.Api.Payments.Queries
{
    public class PaymentQueryResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; }

        [JsonPropertyName("card_number_last_four")]
        public string? CardNumberLast4 { get; }

        [JsonPropertyName("expiry_month")]
        public int ExpiryMonth { get; }

        [JsonPropertyName("expiry_year")]
        public int ExpiryYear { get; }

        [JsonPropertyName("amount")]
        public uint Amount { get; }

        [JsonPropertyName("currency")]
        public string Currency { get; }

        [JsonPropertyName("status")]
        public string Status { get; }

        public PaymentQueryResponse(Guid id, string cardNumberLast4, int expiryMonth, int expiryYear, uint amount, string currency, string status)
        {
            Id = id;
            CardNumberLast4 = cardNumberLast4;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            Amount = amount;
            Currency = currency;
            Status = status;
        }
    }
}
