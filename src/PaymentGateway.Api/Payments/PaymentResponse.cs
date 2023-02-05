namespace PaymentGateway.Api.Payments;

public class PaymentResponse
{
    public Guid Id { get; }

    public string Status { get; }

    private PaymentResponse(string status)
    {
        Id = Guid.NewGuid();
        Status = status;
    }

    public PaymentResponse BuildAuthorizedResponse() => new (PaymentStatus.Authorized);

    public PaymentResponse BuildDeclinedResponse() => new(PaymentStatus.Declined);
}
