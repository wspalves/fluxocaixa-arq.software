using FluentValidation;

namespace FluxoCaixaArq.FluxoCaixa.Application.Commands.Validator;

public class CreditarLancamentoValidator : AbstractValidator<CreditarLancamentoCommand>
{
    public CreditarLancamentoValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("A descrição do lançamento deve ser preenchida.");

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("O valor do lançamento deve ser maior que zero.");
    }
}