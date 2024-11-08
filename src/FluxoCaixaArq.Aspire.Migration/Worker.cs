using FluxoCaixaArq.FluxoCaixa.Data;
using FluxoCaixaArq.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OpenTelemetry.Trace;
using System.Diagnostics;
using static FluxoCaixaArq.FluxoCaixa.Domain.Entities.Lancamento;

namespace FluxoCaixaArq.Aspire.Migration;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<FluxoCaixaContext>();

            await EnsureDatabaseAsync(dbContext, cancellationToken);
            await RunMigrationAsync(dbContext, cancellationToken);
            await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task EnsureDatabaseAsync(FluxoCaixaContext dbContext, CancellationToken cancellationToken)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Create the database if it does not exist.
            // Do this first so there is then a database to start a transaction against.
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    private static async Task RunMigrationAsync(FluxoCaixaContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }

    private static async Task SeedDataAsync(FluxoCaixaContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Seed the database
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Lancamentos.AddRangeAsync(Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 35.99m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 159.90m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 450, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 329.50m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 159.99m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 259.80m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 129.90m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 59.99m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 10, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 85.50m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Débito seed", -10, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Débito seed", -15, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Débito seed", -15, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Débito seed", -10, DateTime.Today.AddDays(-1)));
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
