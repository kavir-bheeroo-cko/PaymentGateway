using MediatR;
using OneOf;

namespace PaymentGateway.Api.Payments;

public class PaymentHandler : IRequestHandler<PaymentRequest,  OneOf<PaymentResponse, Exception>>
{
    public PaymentHandler()
    {

    }

    public async Task<OneOf<PaymentResponse, Exception>> Handle(PaymentRequest request, CancellationToken cancellationToken)
    {
        // validate

        // send to bank

        // create payment

        // store in DB

        // dispatch event

        // build response

        throw new NotImplementedException();
    }
}
