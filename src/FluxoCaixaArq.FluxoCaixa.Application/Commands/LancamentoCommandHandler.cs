using System.Text;
using FluxoCaixaArq.Core.Communication.Mediator;
using FluxoCaixaArq.Core.DomainObjects;
using FluxoCaixaArq.Core.Messages;
using FluxoCaixaArq.FluxoCaixa.Domain.Entities;
using FluxoCaixaArq.FluxoCaixa.Domain.Interfaces;
using MediatR;

namespace FluxoCaixaArq.FluxoCaixa.Application.Commands;

public class LancamentoCommandHandler :
    IRequestHandler<CreditarLancamentoCommand, bool>,
    IRequestHandler<DebitarLancamentoCommand, bool>
{
    private readonly IMediatorHandler _mediator;
    private readonly ILancamentoRepository _lancamentoRepository;

    public LancamentoCommandHandler(IMediatorHandler mediator, ILancamentoRepository lancamentoRepository)
    {
        _mediator = mediator;
        _lancamentoRepository = lancamentoRepository;
    }

    public async Task<bool> Handle(CreditarLancamentoCommand request, CancellationToken cancellationToken)
    {
        ValidarComando(request);

        var lancamento = Credito.LancamentoFactory.NovoCredito(request.Descricao, request.Valor);

        await _lancamentoRepository.AdicionarAsync(lancamento);
        return await _lancamentoRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(DebitarLancamentoCommand request, CancellationToken cancellationToken)
    {
        ValidarComando(request);

        var lancamento = Debito.LancamentoFactory.NovoDebito(request.Descricao, request.Valor);

        await _lancamentoRepository.AdicionarAsync(lancamento);
        return await _lancamentoRepository.UnitOfWork.Commit();
    }

    private void ValidarComando(Command message)
    {
        if (message.EhValido()) return;

        var mensagens = new StringBuilder();
        mensagens.AppendLine("Erro ao realizar lançamento");
        mensagens.AppendLine();

        foreach (var error in message.ValidationResult.Errors)
        {
            mensagens.AppendLine(error.ErrorMessage);
        }
        
        throw new DomainException(mensagens.ToString());
    }
}