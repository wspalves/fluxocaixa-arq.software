using System.Reflection;

namespace FluxoCaixaArq.FluxoCaixa.API.Extensions;

public static class BuilderExtensions
{
    public static WebApplicationBuilder AddAPI(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        return builder;
    }
}