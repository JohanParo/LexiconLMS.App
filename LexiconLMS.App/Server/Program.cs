using LexiconLMS.App.Server.Data;
using LexiconLMS.App.Server.Extensions;
using LexiconLMS.App.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

namespace LexiconLMS.App
{
	public class Program
	{
		public async static Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
				{
					options.SignIn.RequireConfirmedAccount = false;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireUppercase = false;
					options.Password.RequireDigit = false;
					options.Password.RequiredLength = 3;
					options.Password.RequireLowercase = false;
				})

				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddIdentityServer()
				.AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

			builder.Services.AddAuthentication()
				.AddIdentityServerJwt();

			builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages();

			var app = builder.Build();

			await app.SeedDataAsync();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseIdentityServer();
			app.UseAuthorization();


			app.MapRazorPages();
			app.MapControllers();
			app.MapFallbackToFile("index.html");

			app.Run();
		}
	}
}