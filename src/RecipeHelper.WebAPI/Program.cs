using RecipeHelper.Infrastructure.IoC;
using RecipeHelper.Infrastructure.IoC.Container;
using RecipeHelper.WebAPI.Extensions;
using RecipeHelper.WebAPI.Installers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.File("Logs/RecipeLog-.txt", rollingInterval: RollingInterval.Day));
    
builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddJsonFile($"appsettings.{Environment.MachineName}.json", true, true);

// Add services to the container.

builder.Services.InstallServicesInAssembly(builder.Configuration);

// This IoC container registeres all services in the application. Only one project reference needed.

DependencyContainer.RegisterServices(builder.Services, builder.Configuration);

var app = await builder
    .Build()
    .MigrateProjectDatabases();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExtensisons();
}

app.UserErrorHandlingMiddleware();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseCors("AllowAll");
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthcheck");
    endpoints.MapControllers();
});

await app.RunAsync();
