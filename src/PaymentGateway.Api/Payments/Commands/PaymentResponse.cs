namespace PaymentGateway.Api.Payments.Commands;

public class PaymentResponse
{
    public Guid Id { get; }

    public string Status { get; }

    private PaymentResponse(Guid id, string status)
    {
        Id = id;
        Status = status;
    }

    public static PaymentResponse BuildAuthorizedResponse(Guid id) => new(id, PaymentStatus.Authorized);

    public static PaymentResponse BuildDeclinedResponse(Guid id) => new(id, PaymentStatus.Declined);
}
