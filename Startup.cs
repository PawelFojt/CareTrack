using System.Reflection;
using CareTrack.Application;
using CareTrack.Infrastructure;
using CareTrack.Presentation;

namespace CareTrack.Server;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; set; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(cors => cors.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        }));

        services.AddSignalRCore();
        services.AddSignalR();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var applicationLayer = Assembly.Load("CareTrack.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationLayer));
        
        services
            .AddApplication()
            .AddPresentation()
            .AddInfractructure(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment host)
    {
        
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
        
        
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors(x =>
            x.SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
