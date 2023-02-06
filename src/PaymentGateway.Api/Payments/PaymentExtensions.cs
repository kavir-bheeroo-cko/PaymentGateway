using PaymentGateway.Api.Payments.Commands;

namespace PaymentGateway.Api.Payments;

public static class PaymentExtensions
{
    public static PaymentEvent ToEvent(this Payment payment) =>
        new(
            payment.Id,
            payment.Card.Number[^4..],
            payment.Card.ExpiryMonth,
            payment.Card.ExpiryYear,
            payment.Amount,
            payment.Currency,
            payment.Status);
}
