var builder = WebApplication.CreateBuilder(args);

builder
    .AddDBContext(builder.Configuration)
    .AddAPI()
    .AddServices()
    .AddLog();

var app = builder.Build();

app.UseAPI();

app.MapPost("v1/creditar", async (IMediatorHandler _mediatorHandler, decimal valor) =>
    {
        var command = new CreditarLancamentoCommand("Crédito de valor", valor);
        if (await _mediatorHandler.EnviarComando(command))
            return Results.Ok();

        return Results.BadRequest();
    })
    .WithName("PostCreditar")
    .WithOpenApi();

app.MapPost("v1/debitar", async (IMediatorHandler _mediatorHandler, decimal valor) =>
    {
        var command = new DebitarLancamentoCommand("Débito de valor", valor);
        if (await _mediatorHandler.EnviarComando(command))
            return Results.Ok();

        return Results.BadRequest();
    })
    .WithName("PostDebitar")
    .WithOpenApi();

app.Run();
Log.CloseAndFlush();