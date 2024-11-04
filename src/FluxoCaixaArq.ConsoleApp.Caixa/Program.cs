using System.Reflection;
using FluxoCaixaArq.ConsoleApp.Caixa;
using FluxoCaixaArq.ConsoleApp.Caixa.Setup;
using FluxoCaixaArq.FluxoCaixa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var services = new ServiceCollection();

services.AddDbContext<FluxoCaixaContext>(options =>
    options.UseInMemoryDatabase("FluxoCaixaDB"));

services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

Serilog.Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\")) + "log.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

services.AddDependencyInjection();

var serviceProvider = services.BuildServiceProvider();

// Execução
FluxoCaixaContext.Seed(serviceProvider.GetService<FluxoCaixaContext>());
var caixa = serviceProvider.GetService<Caixa>();
await caixa!.IniciarCaixa();