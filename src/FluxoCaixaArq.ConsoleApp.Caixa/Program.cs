// See https://aka.ms/new-console-template for more information

using System.Reflection;
using FluxoCaixaArq.ConsoleApp.Caixa.AppServices;
using FluxoCaixaArq.ConsoleApp.Caixa.Setup;
using FluxoCaixaArq.FluxoCaixa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

services.AddDependencyInjection();

var serviceProvider = services.BuildServiceProvider();

// Execução

bool continuar = true;
var caixaService = serviceProvider.GetService<CaixaService>();

while (continuar)
{
    Console.Clear(); // Limpa a tela
    Console.WriteLine(" --- Caixa --- ");
    Console.WriteLine("Menu Principal:");
    Console.WriteLine("1. Creditar valor");
    Console.WriteLine("2. Debitar valor");
    Console.WriteLine("3. Consolidado diario");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");

    string entrada = Console.ReadLine();

    switch (entrada)
    {
        case "1":
            await Opcao1();
            break;
        case "2":
            await Opcao2();
            break;
        case "3":
            Opcao3();
            break;
        case "0":
            continuar = false;
            break;
        default:
            Console.WriteLine("Opção inválida! Pressione qualquer tecla para tentar novamente.");
            Console.ReadKey();
            break;
    }
}

async Task Opcao1()
{
    Console.WriteLine("Informe o valor que deseja creditar");
    var numerico = decimal.TryParse(Console.ReadLine(), out decimal valor);
    if (!numerico)
        Opcao1();

    var resultado = await caixaService.Creditar(valor);

    Console.WriteLine(!resultado ? "Desculpe, não foi possível realizar o crédito!" : "Crédito realizado com sucesso!");
    Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
    Console.ReadKey();
}

async Task Opcao2()
{
    Console.WriteLine("Informe o valor que deseja debitar");
    var numerico = decimal.TryParse(Console.ReadLine(), out decimal valor);
    if (!numerico)
        Opcao2();

    var resultado = await caixaService.Debitar(valor);

    Console.WriteLine(!resultado ? "Desculpe, não foi possível realizar o débito!" : "Débito realizado com sucesso!");
    Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
    Console.ReadKey();
}

static void Opcao3()
{
    Console.WriteLine("Você escolheu a Opção 3.");
    // Adicione a lógica para a Opção 3 aqui.
    Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
    Console.ReadKey();
}