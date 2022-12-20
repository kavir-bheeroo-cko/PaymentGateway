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
        var cardDetails = CardDetails.CreateFromCardNumber(request.CardNumber);

        return new ProcessCardPaymentResponse(status, Guid.NewGuid(), cardDetails);
    }
}

public record CurrencyAmount(decimal Amount, string CurrencyCode);

public record ExpiryDate(int Month, int Year);

public record ProcessCardPaymentRequest(string CardNumber, string CVV, ExpiryDate ExpiryDate,  CurrencyAmount Amount);

public record CardDetails(string FirstSixDigits, string LastFourDigits)
{
    public static CardDetails CreateFromCardNumber(string cardNumber)
    {
        var firstSixDigits = cardNumber.Substring(0, 6);
        var lastFourDigits = cardNumber.Substring(cardNumber.Length - 4, 4);

        return new CardDetails(firstSixDigits, lastFourDigits);
    }
}

public record ProcessCardPaymentResponse(string Status, Guid PaymentId, CardDetails CardDetails);
