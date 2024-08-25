using System.Reflection;
using CareTrack.Server.Helpers;
using CareTrack.Server.Modules.Domain.Repositories;
using CareTrack.Server.Modules.Infrastructure.presistance;
using CareTrack.Server.Modules.Infrastructure.Repositories;
using DateOnlyTimeOnly.AspNet.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

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
        DotNetEnv.Env.Load();
        services.AddEntityFrameworkNpgsql().AddDbContext<CareTrackDbContext>(options =>
            options.UseNpgsql(ConnectionHelper.GetConnectionString(Configuration)));
        
        services.AddScoped<IMedicineRepository, MedicineRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            });
        services.AddSignalRCore();
        services.AddSignalR();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CareTrack", Version = "v1" });
            c.MapType<DateOnly>(() => new OpenApiSchema { 
                Type = "string",
                Format = "date" 
            });
            c.MapType<TimeOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("HH:mm:ss")
            });
        });

        var applicationLayer = Assembly.Load("CareTrack.Server");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationLayer));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment host)
    {
        if (host.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        
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
