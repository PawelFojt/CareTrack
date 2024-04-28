using CareTrack.Application;
using CareTrack.Infrastructure;
using CareTrack.Infrastructure.presistance;
using CareTrack.Presentation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CareTrack.Server;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; set; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
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

        services.AddEntityFrameworkNpgsql().AddDbContext<CareTrackDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("CareTrackDb")));

        services
            .AddApplication()
            .AddPresentation()
            .AddInfractructure();
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
    }
}
