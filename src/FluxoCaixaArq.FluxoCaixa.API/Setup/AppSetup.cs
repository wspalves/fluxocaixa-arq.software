namespace FluxoCaixaArq.FluxoCaixa.API.Setup;

public static class AppSetup
{
    public static WebApplication UseAPI(this WebApplication app)
    {
        app.UseHttpsRedirection();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
        }

        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}