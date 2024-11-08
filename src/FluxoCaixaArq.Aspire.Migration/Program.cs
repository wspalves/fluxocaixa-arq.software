using FluxoCaixaArq.Aspire.Migration;
using FluxoCaixaArq.FluxoCaixa.Data;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddSqlServerDbContext<FluxoCaixaContext>("FluxoCaixaDB");

var host = builder.Build();
host.Run();
