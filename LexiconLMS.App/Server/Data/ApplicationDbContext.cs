using Duende.IdentityServer.EntityFramework.Options;
using LexiconLMS.App.Server;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LexiconLMS.App.Server.Data
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public ApplicationDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}
		public DbSet<LexiconLMS.App.Server.Course> Course { get; set; } = default!;
		public DbSet<LexiconLMS.App.Server.Activity> Activity { get; set; } = default!;
		public DbSet<LexiconLMS.App.Server.ActivityType> ActivityType { get; set; } = default!;
		public DbSet<LexiconLMS.App.Server.Module> Module { get; set; } = default!;
		public DbSet<LexiconLMS.App.Server.Document> Document { get; set; } = default!;
		public DbSet<LexiconLMS.App.Server.DocumentType> DocumentType { get; set; } = default!;
	}
}