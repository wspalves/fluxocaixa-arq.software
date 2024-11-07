using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddDBContext(builder.Configuration)
    .AddAPI()
    .AddServices()
    .AddLog();

var app = builder.Build();

app.UseAPI();

#region POST Creditar

app.MapPost("v1/creditar", async (IMediatorHandler _mediatorHandler, [FromBody] LancamentoViewModel body) =>
    {
        var command = new CreditarLancamentoCommand("Crédito de valor", body.Valor);
        if (await _mediatorHandler.EnviarComando(command))
            return Results.Ok();

        return Results.BadRequest();
    })
    .WithName("PostCreditar")
    .WithOpenApi();

#endregion

#region POST Debitar

app.MapPost("v1/debitar", async (IMediatorHandler _mediatorHandler, [FromBody] LancamentoViewModel body) =>
    {
        var command = new DebitarLancamentoCommand("Débito de valor", body.Valor);
        if (await _mediatorHandler.EnviarComando(command))
            return Results.Ok();

        return Results.BadRequest();
    })
    .WithName("PostDebitar")
    .WithOpenApi();

#endregion

app.Run();
Log.CloseAndFlush();