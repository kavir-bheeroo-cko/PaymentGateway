using MediatR;

namespace PaymentGateway.Api.Payments;

public record PaymentEvent : INotification
{
    public Guid Id { get; }

    public string? CardNumberLast4 { get; }

    public int ExpiryMonth { get; }

    public int ExpiryYear { get; }

    public uint Amount { get; }

    public string Currency { get; }

    public string Status { get; }

    public PaymentEvent(
        Guid id,
        string? cardNumberLast4,
        int expiryMonth,
        int expiryYear,
        uint amount,
        string currency,
        string status)
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
