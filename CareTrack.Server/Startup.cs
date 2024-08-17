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
        if(host.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
