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
    public DateTime DataCadastro { get; protected set; }


    #region Somente para facilitar o SEED em memória

    protected Lancamento(string descricao, decimal valor, DateTime data)
    {
        Descricao = descricao;
        Valor = valor;
        DataCadastro = data;
    }

    public static class LancamentoFactory
    {
        // Somente para facilitar o SEED em memória
        public static Lancamento NovoLancamento(string descricao, decimal valor, DateTime data) =>
            new(descricao, valor, data);
    }

    #endregion
}