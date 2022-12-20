namespace PaymentGateway.Api.UseCases.PaymentProcessing;

public class PaymentProcessingService
{
    private readonly IAcquirerClient _client;
    
    public PaymentProcessingService(IAcquirerClient client)
    {
        _client = client;
    }

    public ProcessCardPaymentResponse ProcessCardPayment(ProcessCardPaymentRequest request)
    {
        var authRequest = new AuthorizePaymentRequest(request.CardNumber, request.CVV, request.ExpiryDate, request.Amount);
        var authResponse = _client.AuthorizePayment(authRequest);
        var status = authResponse.Authorized ? "Authorized" : "Declined";
        var firstSixDigits = request.CardNumber.Substring(0, 6);
        var lastFourDigits = request.CardNumber.Substring(request.CardNumber.Length - 4, 4);

        return new ProcessCardPaymentResponse(status, Guid.NewGuid(), new CardDetails(firstSixDigits, lastFourDigits));
    }
}

public record CurrencyAmount(decimal Amount, string CurrencyCode);

public record ExpiryDate(int Month, int Year);

public record ProcessCardPaymentRequest(string CardNumber, string CVV, ExpiryDate ExpiryDate,  CurrencyAmount Amount);

public record CardDetails(string FirstSixDigits, string LastFourDigits);

public record ProcessCardPaymentResponse(string Status, Guid PaymentId, CardDetails CardDetails);
