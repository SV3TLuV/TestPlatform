using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using TestPlatform.Api.Module;
using TestPlatform.Application.Modules;

var applicationBuilder = WebApplication.CreateBuilder(args);
applicationBuilder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
applicationBuilder.Host
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        var configuration = applicationBuilder.Configuration;
        
        builder.RegisterModule(new ApiModule(configuration));
        builder.RegisterModule<ApplicationModule>();
    })
    .ConfigureServices(services =>
    {
        services
            .AddCors(options => options.AddPolicy("CORS", policy =>
            {
                policy
                    .WithMethods(
                        HttpMethods.Get,
                        HttpMethods.Post,
                        HttpMethods.Put,
                        HttpMethods.Delete)
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true);
            }))
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers()
            .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    });

var app = applicationBuilder.Build();
ConfigureApp(app);
await app.RunAsync();


void ConfigureApp(WebApplication application)
{
    application.UseSwagger();
    application.UseSwaggerUI();
    application.UseCors("CORS");
    application.UseHttpsRedirection();
    application.UseAuthorization();
    application.MapControllers();   
}