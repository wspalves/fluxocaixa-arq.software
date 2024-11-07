using System.Data;
using Dapper;
using FluxoCaixaArq.ConsolidadoCaixa.Application.Interfaces;
using FluxoCaixaArq.ConsolidadoCaixa.Application.ViewModel;
using Microsoft.Data.SqlClient;

namespace FluxoCaixaArq.ConsolidadoCaixa.Data.Repository;

public class ConsolidadoRepository : IConsolidadoRepository
{
    private readonly string connectionString;

    public ConsolidadoRepository(string connString)
    {
        connectionString = connString;
    }

    public async Task<ConsolidadoViewModel> ObterConsolidadoOntemAsync()
    {
        var data = DateTime.Today.AddDays(-1);
        string query = $@"
                SELECT 
                    SUM(valor) AS Valor, 
                    '{data}' as DataCadastro,
                    COUNT(DataCadastro) AS QuantidadeLancamentos 
                FROM dbo.Lancamentos 
                WHERE CAST(DataCadastro AS DATE) = '{data}'";

        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            return await connection.QueryFirstAsync<ConsolidadoViewModel>(query);
        }
    }

    public async Task<ConsolidadoViewModel> ObterConsolidadoPorDataAsync(DateTime data)
    {
        string query = $@"
                SELECT 
                    SUM(valor) AS Valor, 
                    '{data}' as DataCadastro,                     
                    COUNT(DataCadastro) AS QuantidadeLancamentos 
                FROM dbo.Lancamentos 
                WHERE CAST(DataCadastro AS DATE) = '{data.Date}'";

        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            return await connection.QueryFirstAsync<ConsolidadoViewModel>(query);
        }
    }
}