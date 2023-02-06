using FluentValidation;
using FluentValidation.Results;
using MediatR;
using PaymentGateway.Api.Acquirers;
using PaymentGateway.Api.Bank;
using PaymentGateway.Api.Payments;
using PaymentGateway.Api.Payments.Commands;

namespace PaymentGateway.Api.Tests
{
    public class PaymentHandlerTests
    {
        private readonly PaymentHandler _sut;

        private readonly IAcquirer _acquirer;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMediator _mediator;
        private readonly IValidator<PaymentRequest> _validator;

        public PaymentHandlerTests()
        {
            _acquirer = Substitute.For<IAcquirer>();
            _paymentRepository = Substitute.For<IPaymentRepository>();
            _mediator= Substitute.For<IMediator>();
            _validator = Substitute.For<IValidator<PaymentRequest>>();

            _sut = new PaymentHandler(
                _acquirer,
                _paymentRepository,
                _mediator,
                _validator);
        }

        [Theory]
        [AutoData]
        public async Task GivenValidRequest_ShouldReturnAuthorized(PaymentRequest paymentRequest, AcquirerResponse acquirerResponse)
        {
            _acquirer.ProcessPaymentAsync(Arg.Any<AcquirerRequest>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(acquirerResponse));

            _validator.ValidateAsync(Arg.Any<PaymentRequest>())
                .Returns(new ValidationResult());

            var result = await _sut.Handle(paymentRequest, CancellationToken.None);

            result.IsT0.Should().BeTrue();
            result.AsT0.Status.Should().Be(PaymentStatus.Authorized);

            await _acquirer.Received(1)
                .ProcessPaymentAsync(Arg.Any<AcquirerRequest>(), Arg.Any<CancellationToken>());

            await _validator.Received(1)
                .ValidateAsync(Arg.Any<PaymentRequest>());

            await _paymentRepository.Received(1)
                .SaveAsync(Arg.Any<Payment>());

            await _mediator.Received(1)
                .Publish(Arg.Any<PaymentEvent>(), Arg.Any<CancellationToken>());
        }
    }
}
