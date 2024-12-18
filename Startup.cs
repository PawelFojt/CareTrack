using System.Reflection;
using System.Text;
using CareTrack.Server.Helpers;
using CareTrack.Server.Modules.Domain.Repositories;
using CareTrack.Server.Modules.Infrastructure.Hubs;
using CareTrack.Server.Modules.Infrastructure.presistance;
using CareTrack.Server.Modules.Infrastructure.Repositories;
using DateOnlyTimeOnly.AspNet.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
        DotNetEnv.Env.Load();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
        });
        services.AddEntityFrameworkNpgsql().AddDbContext<CareTrackDbContext>(options =>
            options.UseNpgsql(ConnectionHelper.GetConnectionString(Configuration)));
        
        services.AddScoped<IMedicineRepository, MedicineRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        
        var jwtSettings = Configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = key
            };
        });
        
        services.AddAuthorization();
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            });
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
        app.UseCors("AllowAllOrigins");
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
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<ChatHub>("/chathub");
        });
    }
}