using FluxoCaixaArq.Core.Communication.Mediator;
using FluxoCaixaArq.Core.DomainObjects;
using FluxoCaixaArq.FluxoCaixa.Application.Commands;
using FluxoCaixaArq.FluxoCaixa.Domain.Entities;
using FluxoCaixaArq.FluxoCaixa.Domain.Interfaces;
using Moq;

namespace FluxoCaixaArq.FluxoCaixa.Test;

public class LancamentoCommandsTests
{
    private readonly Mock<ILancamentoRepository> _lancamentoRepositoryMock;
    private readonly Mock<IMediatorHandler> _mediatorHandlerMock;

    public LancamentoCommandsTests()
    {
        _mediatorHandlerMock = new Mock<IMediatorHandler>();

        _lancamentoRepositoryMock = new Mock<ILancamentoRepository>();
        _lancamentoRepositoryMock.Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));
    }

    [Fact]
    public async Task Credito_Validar_AdicionarAsyncDeveSerChamado()
    {
        // Arrange
        var handler = new LancamentoCommandHandler(_mediatorHandlerMock.Object, _lancamentoRepositoryMock.Object);
        var command = new CreditarLancamentoCommand("Crédito", 130);

        // Act
        await handler.Handle(command, new CancellationToken());

        // Assert
        _lancamentoRepositoryMock.Verify(repo => repo.AdicionarAsync(It.Is<Lancamento>(l =>
            l.Descricao == command.Descricao && l.Valor == command.Valor)), Times.Once);
    }

    [Fact]
    public async Task Credito_Validar_CreditosComValorNegativoDevemRetornarException()
    {
        // Arrange
        var handler = new LancamentoCommandHandler(_mediatorHandlerMock.Object, _lancamentoRepositoryMock.Object);
        var command = new CreditarLancamentoCommand("Crédito", -130);

        // Act
        var exec = await Assert.ThrowsAsync<DomainException>(async () =>
            await handler.Handle(command, new CancellationToken()));

        // Assert
        Assert.Equal("O valor do lançamento deve ser maior que zero.\r\n",
            exec.Message);
    }
    
    [Fact]
    public async Task Debito_Validar_CommandoDebitoComValorNegativoDevemRetornarException()
    {
        // Arrange
        var handler = new LancamentoCommandHandler(_mediatorHandlerMock.Object, _lancamentoRepositoryMock.Object);
        var command = new DebitarLancamentoCommand("Débito", -130);

        // Act
        var exec = await Assert.ThrowsAsync<DomainException>(async () =>
            await handler.Handle(command, new CancellationToken()));

        // Assert
        Assert.Equal("O valor do lançamento deve ser maior que zero.\r\n",
            exec.Message);
    }
}