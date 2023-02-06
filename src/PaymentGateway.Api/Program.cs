using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using OneOf;
using PaymentGateway.Api.Acquirers;
using PaymentGateway.Api.Acquirers.BankSimulator;
using PaymentGateway.Api.Bank;
using PaymentGateway.Api.Payments;
using PaymentGateway.Api.Payments.Commands;
using PaymentGateway.Api.Payments.Commands.InMemoryRepository;
using PaymentGateway.Api.Payments.Queries;
using PaymentGateway.Api.Payments.Queries.InMemoryRepository;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddMediatR(typeof(Payment).GetTypeInfo().Assembly);
    builder.Services.AddOptions<AcquirerOptions>().Bind(builder.Configuration.GetSection(AcquirerOptions.SectionName));
    builder.Services.AddHttpClient();
    builder.Services.AddValidatorsFromAssemblyContaining<PaymentRequestValidator>();
    builder.Services.AddSingleton<IPaymentRepository, InMemoryPaymentRepository>();
    builder.Services.AddSingleton<IAcquirer, BankSimulatorAcquirer>();
    builder.Services.AddSingleton<IPaymentEventRepository, InMemoryPaymentEventRepository>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseHttpsRedirection();

    //app.UseAuthorization();

    app.UseRouting();

    app.MapPost("/payments", async (PaymentRequest request, IMediator mediator, CancellationToken cancellationToken) =>
    { 
        var result = await mediator.Send(request, cancellationToken);
        return MatchResult(result);
    });

    app.MapGet("/payments/{paymentId}", async (Guid paymentId, IMediator mediator, CancellationToken cancellationToken) =>
    {
        var queryRequest = new PaymentQueryRequest(paymentId);
        var queryResult = await mediator.Send(queryRequest, cancellationToken);
        return MatchQueryResult(queryResult);
    });

    app.Run();

    static IResult MatchResult(OneOf<PaymentResponse, ValidationResult, Exception> result)
    {
        return result.Match(
            created => Results.Json(statusCode: 201, data: created),
            validationError => Results.Json(statusCode: 400, data: validationError.Errors.Select(x => x.ErrorMessage )),
            genericError => Results.Json(statusCode: 500, data: genericError));
    }

    static IResult MatchQueryResult(OneOf<PaymentQueryResponse?, Exception> result)
    {
        return result.Match(
            response =>
            {
                if (response is not null)
                {
                    return Results.Json(statusCode: 200, data: response);
                }
                else
                {
                    return Results.NotFound();
                }
            },
            genericError => Results.Json(statusCode: 500, data: genericError));
    }
}
catch (Exception exception)
{
    Log.Fatal(exception, "Payment Gateway terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
    await Task.Delay(TimeSpan.FromMilliseconds(200));
}