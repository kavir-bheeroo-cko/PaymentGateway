using System.Text.Json.Serialization;

namespace PaymentGateway.Api.Acquirers;

public record AcquirerRequest
{
    [JsonPropertyName("card_number")]
    public string CardNumber { get; }

    [JsonPropertyName("expiry_date")]
    public string ExpiryDate { get; }

    [JsonPropertyName("amount")]
    public uint Amount { get; }

    [JsonPropertyName("currency")]
    public string Currency { get; }

    [JsonPropertyName("cvv")]
    public string Cvv { get; }

    public AcquirerRequest(string cardNumber, int expiryMonth, int expiryYear, uint amount, string currency, string cvv)
    {
        string expiryMonthInString = expiryMonth.ToString().PadLeft(2, '0');

        CardNumber = cardNumber;
        ExpiryDate = $"{expiryMonthInString}/{expiryYear}";
        Amount = amount;
        Currency = currency;
        Cvv = cvv;
    }
}
