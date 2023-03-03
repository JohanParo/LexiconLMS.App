using LexiconLMS.App.Server.Data;

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

                //db.Database.EnsureDeleted();
                //db.Database.Migrate();

                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var adminPW = config["AdminPW"];  // user-secrets

                ArgumentNullException.ThrowIfNull(adminPW, nameof(adminPW));

                try
                {
                    await SeedData.InitAsync(db, serviceProvider, adminPW);
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
