using Microsoft.EntityFrameworkCore;
using User_Authentication___Organisation_with_Unit_of_Work.Models;

namespace User_Authentication___Organisation_with_Unit_of_Work.Persistence
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Organization> Organizations { get; set; }
		public DbSet<UserOrganization> UserOrganizations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserOrganization>()
				.HasKey(uo => new { uo.UserId, uo.OrgId });

			modelBuilder.Entity<UserOrganization>()
				.HasOne(uo => uo.User)
				.WithMany(u => u.UserOrganizations)
				.HasForeignKey(uo => uo.UserId);

			modelBuilder.Entity<UserOrganization>()
				.HasOne(uo => uo.Organization)
				.WithMany(o => o.UserOrganizations)
				.HasForeignKey(uo => uo.OrgId);

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email)
				.IsUnique();

			modelBuilder.Entity<User>()
				.HasIndex(u => u.UserId)
				.IsUnique();
		}
	}
}
