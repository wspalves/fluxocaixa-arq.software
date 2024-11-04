// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Reflection;
using FluxoCaixaArq.ConsoleApp.Caixa;
using FluxoCaixaArq.ConsoleApp.Caixa.AppServices;
using FluxoCaixaArq.ConsoleApp.Caixa.Setup;
using FluxoCaixaArq.ConsolidadoCaixa.Application.Queries;
using FluxoCaixaArq.FluxoCaixa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var services = new ServiceCollection();

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

services.AddDbContext<FluxoCaixaContext>(options =>
    options.UseSqlServer(connectionString));

services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

Serilog.Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\")) + "log.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

services.AddDependencyInjection();

var serviceProvider = services.BuildServiceProvider();

// Execução
var caixa = serviceProvider.GetService<Caixa>();
await caixa!.IniciarCaixa();