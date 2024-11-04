using System.Globalization;
using FluxoCaixaArq.ConsoleApp.Caixa.AppServices;
using Serilog;

namespace FluxoCaixaArq.ConsoleApp.Caixa;

public class Caixa
{
    private readonly CaixaService _caixaService;


    public Caixa(CaixaService caixaService)
    {
        _caixaService = caixaService;
    }

    public async Task IniciarCaixa()
    {
        bool continuar = true;
        while (continuar)
        {
            Log.Information("Iniciando log");

            MenuCaixa();

            string entrada = Console.ReadLine();
            switch (entrada)
            {
                case "1":
                    await Creditar();
                    break;
                case "2":
                    await Debitar();
                    break;
                case "3":
                    await ConsolidadoDiaAnterior();
                    break;
                case "4":
                    await ConsolidadoData();
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
    }

    private void MenuCaixa()
    {
        Console.Clear();
        Console.WriteLine(" --- Caixa --- ");
        Console.WriteLine("Menu Principal:");
        Console.WriteLine("1. Creditar valor");
        Console.WriteLine("2. Debitar valor");
        Console.WriteLine("3. Consolidado do dia anterior");
        Console.WriteLine("4. Consolidado por data");
        Console.WriteLine("0. Sair");
        Console.Write("Escolha uma opção: ");
    }

    async Task Creditar()
    {
        Console.WriteLine("Informe o valor que deseja creditar");
        var numerico = decimal.TryParse(Console.ReadLine(), out decimal valor);
        if (!numerico)
            Creditar();

        var resultado = await _caixaService.Creditar(valor);

        Console.WriteLine(!resultado
            ? "Desculpe, não foi possível realizar o crédito!"
            : "Crédito realizado com sucesso!");
        Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }

    async Task Debitar()
    {
        Console.WriteLine("Informe o valor que deseja debitar");
        var numerico = decimal.TryParse(Console.ReadLine(), out decimal valor);
        if (!numerico)
            Debitar();

        var resultado = await _caixaService.Debitar(valor);

        Console.WriteLine(
            !resultado ? "Desculpe, não foi possível realizar o débito!" : "Débito realizado com sucesso!");
        Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }

    async Task ConsolidadoDiaAnterior()
    {
        var viewModel = await _caixaService.ConsolidadoDiaAnterior();

        Console.WriteLine($" --- Consolidado de {viewModel.DataCadastro:dd/MM/yyyy} ---");
        Console.WriteLine($"Valor Total: {viewModel.Valor.ToString("C", new CultureInfo("pt-BR"))}");
        Console.WriteLine($"Quantidade de lançamentos: {viewModel.QuantidadeLancamentos}");
        Console.WriteLine("");

        // Adicione a lógica para a Opção 3 aqui.
        Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }

    async Task ConsolidadoData()
    {
        Console.WriteLine("Informe a data que deseja consultar. Por exemplo: 20/12/2024");
        var dateTime = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", new CultureInfo("pt-BR"),
            DateTimeStyles.None, out DateTime date);
        if (!dateTime)
        {
            Console.WriteLine("Data em formato inválido!");
            ConsolidadoData();
        }

        var viewModel = await _caixaService.ConsolidadoData(date);

        Console.WriteLine($" --- Consolidado de {viewModel.DataCadastro:dd/MM/yyyy} ---");
        Console.WriteLine($"Valor Total: {viewModel.Valor.ToString("C", new CultureInfo("pt-BR"))}");
        Console.WriteLine($"Quantidade de lançamentos: {viewModel.QuantidadeLancamentos}");
        Console.WriteLine("");

        // Adicione a lógica para a Opção 3 aqui.
        Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }
}