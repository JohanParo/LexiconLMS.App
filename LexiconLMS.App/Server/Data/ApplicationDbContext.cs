using Duende.IdentityServer.EntityFramework.Options;
using LexiconLMS.Shared.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LexiconLMS.App.Shared.Entities;

namespace LexiconLMS.App.Server.Data
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public ApplicationDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}
		public DbSet<LexiconLMS.Shared.Entities.Course> Course { get; set; } = default!;
		public DbSet<LexiconLMS.Shared.Entities.Activity> Activity { get; set; } = default!;
		public DbSet<LexiconLMS.Shared.Entities.ActivityType> ActivityType { get; set; } = default!;
		public DbSet<LexiconLMS.Shared.Entities.Module> Module { get; set; } = default!;
		public DbSet<LexiconLMS.Shared.Entities.Document> Document { get; set; } = default!;
		public DbSet<LexiconLMS.App.Shared.Entities.DocumentType> DocumentType { get; set; } = default!;
	}
}