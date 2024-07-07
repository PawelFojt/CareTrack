using CareTrack.Domain.Models;
using CareTrack.Domain.Repositories;
using CareTrack.Infrastructure.presistance;
using CareTrack.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareTrack.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfractructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEntityFrameworkNpgsql().AddDbContext<CareTrackDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CareTrackDb")));
        services.AddScoped<IMedicineRepository, MedicineRepository>();
        return services;
    }
}
