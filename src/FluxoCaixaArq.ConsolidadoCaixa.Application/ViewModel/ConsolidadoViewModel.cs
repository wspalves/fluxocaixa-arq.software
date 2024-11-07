namespace FluxoCaixaArq.ConsolidadoCaixa.Application.ViewModel;

public record ConsolidadoViewModel
{
    public decimal Valor { get; set; }
    public DateTime DataCadastro { get; set; }
    public int QuantidadeLancamentos { get; set; }
}