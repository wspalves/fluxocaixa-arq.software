namespace FluxoCaixaArq.FluxoCaixa.API.Extensions;

public static class LogExtensions
{
    public static WebApplicationBuilder AddLog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger();

        return builder;
    }
}