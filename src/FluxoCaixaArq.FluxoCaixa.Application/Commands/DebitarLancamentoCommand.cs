using FluxoCaixaArq.Core.Messages;
using FluxoCaixaArq.FluxoCaixa.Application.Commands.Validator;

namespace FluxoCaixaArq.FluxoCaixa.Application.Commands;

public class DebitarLancamentoCommand : Command
{
    public DebitarLancamentoCommand(string descricao, decimal valor)
    {
        Descricao = descricao;
        Valor = valor;
    }

    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }

    public override bool EhValido()
    {
        ValidationResult = new DebitarLancamentoValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}