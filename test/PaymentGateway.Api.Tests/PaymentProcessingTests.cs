using PaymentGateway.Api.UseCases.PaymentProcessing;

namespace PaymentGateway.Api.Tests;

public class PaymentProcessingTests
{
    [Fact]
    public void When_processing_a_payment_with_valid_details_then_it_is_accepted()
    {
        var expiryDate = DateTime.Now.AddYears(1);
        var cardDetails = new ProcessCardPaymentRequest
        (
            CardNumber: "2222405343248877", // Dummy MasterCard number
            CVV: "123",
            ExpiryDate: new ExpiryDate(expiryDate.Month, expiryDate.Year),
            Amount: new CurrencyAmount(100, "GBP")
        );

        var stubAcquirer = new StubAcquirerClient();
        var sut = new PaymentProcessingService(stubAcquirer);
        var result = sut.ProcessCardPayment(cardDetails);
        
        Assert.Equal("Authorized", result.Status);
        Assert.NotEqual(Guid.Empty, result.PaymentId);
        Assert.Equal("222240", result.CardDetails.FirstSixDigits);
        Assert.Equal("8877", result.CardDetails.LastFourDigits);
    }
}

public class StubAcquirerClient : IAcquirerClient
{
    private Dictionary<string, AuthorizePaymentResponse> responses = new ()
    {
        {"2222405343248877", new AuthorizePaymentResponse(true, Guid.NewGuid().ToString("D"))}
    };

    public AuthorizePaymentResponse AuthorizePayment(AuthorizePaymentRequest request)
    {
        if (!responses.TryGetValue(request.CardNumber, out var response))
        {
            return new AuthorizePaymentResponse(false, string.Empty);
        }
        return response;
    }
}