using System.Reflection;

namespace FluxoCaixaArq.FluxoCaixa.API.Setup;

public static class BuilderSetup
{
    public static WebApplicationBuilder AddAPI(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Fluxo de Caixa - Arquitetura de Software",
                Description = "Esta API faz parte da solução Fluxo de Caixa - Arquitetura de Software.",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    { Name = "Wesley Alves", Email = "wspalves@gmail.com" }
            });
        });
        
        return builder;
    }
}