using LexiconLMS.App.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace LexiconLMS.App.Server.Extensions
{
    public static class ApplicationBuilderExtension
    {

        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureDeleted();
                db.Database.Migrate();

                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var password = "Bytmig123!"; //config["AdminPW"];  // user-secrets

                ArgumentNullException.ThrowIfNull(password, nameof(password));

                try
                {
                    await SeedData.InitAsync(db, serviceProvider, password);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }

        }

    }
}
