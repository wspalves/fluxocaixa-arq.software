using FluxoCaixaArq.Core.DomainObjects;

namespace FluxoCaixaArq.FluxoCaixa.Domain.Entities;

public class Lancamento : Entity, IAggregateRoot
{
    protected Lancamento()
    {
    }

    protected Lancamento(string descricao, decimal valor)
    {
        Descricao = descricao;
        Valor = valor;
    }

    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }

    public DateTime DataCadastro { get; private set; }
}