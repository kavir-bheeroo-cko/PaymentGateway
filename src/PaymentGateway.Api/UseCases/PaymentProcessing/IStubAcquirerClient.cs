namespace PaymentGateway.Api.UseCases.PaymentProcessing;

public interface IAcquirerClient
{
    AuthorizePaymentResponse AuthorizePayment(AuthorizePaymentRequest request);
}

public record AuthorizePaymentRequest(string CardNumber, string CVV, ExpiryDate ExpiryDate,  CurrencyAmount Amount);

public record AuthorizePaymentResponse(bool Authorized, string AuthorizationCode);