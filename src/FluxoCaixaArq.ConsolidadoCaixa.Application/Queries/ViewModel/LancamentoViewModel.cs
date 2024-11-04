namespace FluxoCaixaArq.ConsolidadoCaixa.Application.Queries.ViewModel;

public record LancamentoViewModel
{
    public decimal Valor { get; set; }
    public DateTime DataCadastro { get; set; }
    public int QuantidadeLancamentos { get; set; }
}