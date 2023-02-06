using MediatR;
using OneOf;
using Serilog;
using PaymentGateway.Api.Payments;
using PaymentGateway.Api.Payments.InMemoryRepository;
using PaymentGateway.Api.Bank;
using PaymentGateway.Api.Acquirers.BankSimulator;
using System.Reflection;
using PaymentGateway.Api.Acquirers;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddMediatR(typeof(Payment).GetTypeInfo().Assembly);
    builder.Services.AddOptions<AcquirerOptions>().Bind(builder.Configuration.GetSection(AcquirerOptions.SectionName));
    builder.Services.AddHttpClient();
    builder.Services.AddSingleton<IPaymentRepository, InMemoryPaymentRepository>();
    builder.Services.AddSingleton<IAcquirer, BankSimulatorAcquirer>();

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

    app.MapGet("/payments", () => Results.Ok());

    app.Run();

    static IResult MatchResult(OneOf<PaymentResponse, Exception> result)
    {
        return result.Match(
            created => Results.Json(statusCode: 201, data: created),
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