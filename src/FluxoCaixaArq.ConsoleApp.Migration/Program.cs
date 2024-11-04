// See https://aka.ms/new-console-template for more information

using System.Reflection;
using FluxoCaixaArq.ConsoleApp.Caixa.Setup;
using FluxoCaixaArq.FluxoCaixa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var services = new ServiceCollection();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((config) =>
    {
        config.AddJsonFile("appsettings.json");
        config.AddEnvironmentVariables();
        config.Build();
    })
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<FluxoCaixaContext>(options => options.UseSqlServer(connectionString));
    })
    .Build();

services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

services.AddDependencyInjection();

services.BuildServiceProvider();

host.Run();