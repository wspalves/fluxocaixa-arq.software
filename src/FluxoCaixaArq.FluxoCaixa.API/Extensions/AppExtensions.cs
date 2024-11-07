namespace FluxoCaixaArq.FluxoCaixa.API.Extensions;

public static class AppExtensions
{
    public static WebApplication UseAPI(this WebApplication app)
    {
        app.UseHttpsRedirection();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}