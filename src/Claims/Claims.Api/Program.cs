using Claims.Api;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

try
{

    Log.Information("Starting web host");
    var builder = WebApplication.CreateBuilder(args);
    {
        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());

        builder.Services.AddControllers();
        builder.Services.AddSharedFrameworkServices(builder.Configuration)
                        .AddModuleServices(builder.Configuration);
    }
   var app = builder.Build();
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
                c.DisplayRequestDuration();
            });
        }

        app.UseHttpsRedirection();
        app.MapControllers();


        app.Run();
    }


}
catch (Exception ex)
{
    Log.Fatal(ex, "Web host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}