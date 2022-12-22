# Building a payment gateway
This template is provided to help bootstrap your solution structure so you can focus on
the implementation details of your code test submission.

## Template structure
```
src/
    PaymentGateway.Api - a skeleton ASP.NET Core Web API
test/
    PaymentGateway.Api.Tests - an empty xUnit test project
imposters/ - contains the bank simulator configuration. Don't change this

.editorconfig - don't change this. It ensures a consistent set of rules for submissions when reformatting code
docker-compose.yml - configures the bank simulator
PaymentGateway.sln
```

Feel free to change the structure of the solution, use a different test library etc.

## Bank simulator
A bank simulator is provided. The simulator provides responses based on a set of known test cards, 
each of which return a specific response so that successful authorizations and declines can be tested.  

### Starting the simulator
To start the simulator, run `docker-compose up`

### Calling the simulator

The simulator supports a single route which is a HTTP POST to the following URI:
```
http://localhost:8080/payments
```

The JSON snippet below shows an example of the body that is expected to be submitted:

```json
{
  "card_number": "2222405343248877",
  "expiry_date": "04/2025",
  "currency": "GBP",
  "amount": 100,
  "cvv": 123
}
```
A response will be provided with the following structure:

```json
{
  "authorized": false,
  "authorization_code": "0bb07405-6d44-4b50-a14f-7ae0beff13ad"
}
```

### Test cards
The simulator supports the following card details. If these are not provided a HTTP 400 response will be returned

| Card number      | Expiry date | Currency | Amount | CVV | Authorized  | Authorization code                 |
|------------------|-------------|----------|--------|-----|-------------|------------------------------------|
| 2222405343248877 | 04/2025     | GBP      | 100    | 123 | true        | 0bb07405-6d44-4b50-a14f-7ae0beff13ad |
| 2222405343248112 | 01/2026     | USD      | 60000  | 456 | false       | < empty >                          |