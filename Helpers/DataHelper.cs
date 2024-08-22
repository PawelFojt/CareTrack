using CareTrack.Server.presistance;
using Microsoft.EntityFrameworkCore;

namespace CareTrack.Server.Helpers;

public static class DataHelper
{
    public static async Task ManageDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CareTrackDbContext>();
        await context.Database.MigrateAsync();
    }
}