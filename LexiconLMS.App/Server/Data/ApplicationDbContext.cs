using Duende.IdentityServer.EntityFramework.Options;
using LexiconLMS.App.Server.Models;
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
		public DbSet<Course> Course { get; set; } = default!;
		public DbSet<Activity> Activity { get; set; } = default!;
		public DbSet<ActivityType> ActivityType { get; set; } = default!;
		public DbSet<Module> Module { get; set; } = default!;
		public DbSet<Document> Document { get; set; } = default!;
		public DbSet<DocumentType> DocumentType { get; set; } = default!;
	}
}