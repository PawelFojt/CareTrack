using CareTrack.Server.Modules.Infrastructure.presistance;
using Microsoft.EntityFrameworkCore;

namespace CareTrack.Server.Helpers;

public static class DataHelper
{
    public static async Task ManageDataAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<CareTrackDbContext>();
        await context.Database.MigrateAsync();
    }
}