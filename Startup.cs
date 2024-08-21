using System.Reflection;
using CareTrack.Server.presistance;
using CareTrack.Server.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CareTrack.Server;

public class Startup
{
    private IConfiguration Configuration { get; set; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(cors => cors.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        }));

        // var assembly = typeof(DependencyInjectionExtensions).Assembly;
        // services.AddMediatR(configuration =>
        //     configuration.RegisterServicesFromAssembly(assembly));
        //
        // services.AddValidatorsFromAssembly(assembly);
        
        services.AddEntityFrameworkNpgsql().AddDbContext<CareTrackDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("CareTrackDb")));
        
        services.AddScoped<IMedicineRepository, MedicineRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        
        services.AddControllers();
        services.AddSignalRCore();
        services.AddSignalR();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var applicationLayer = Assembly.Load("CareTrack.Server");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationLayer));
        
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
