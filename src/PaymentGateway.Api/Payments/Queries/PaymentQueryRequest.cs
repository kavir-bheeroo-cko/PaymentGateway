using MediatR;
using OneOf;

namespace PaymentGateway.Api.Payments.Queries;

public record PaymentQueryRequest : IRequest<OneOf<PaymentQueryResponse?, Exception>>
{
    public Guid Id { get; }

    public PaymentQueryRequest(Guid id)
    {
        Id = id;
    }
}
