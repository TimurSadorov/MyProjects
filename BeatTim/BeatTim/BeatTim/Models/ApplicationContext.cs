using Microsoft.EntityFrameworkCore;

namespace BeatTim.Models
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Client> Clients { get; set; }
		public DbSet<Beat> Beats { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Follower> Followers { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<LoginDetails> LoginDetails { get; set; }
		public DbSet<UserComment> UserComments { get; set; }
		public DbSet<UserProfile> UserProfiles { get; set; }
		public DbSet<AccessToken> AccessTokens { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Beat>()
				.HasCheckConstraint("beat_price_greater_zero", "\"Price\" >= 0")
				.HasCheckConstraint("beat_number_auditions_greater_zero", "\"NumberAuditions\" >= 0")
				.HasCheckConstraint("beat_rating_greater_zero", "\"Rating\" >= 0 AND \"Rating\" <= 10")
				.HasOne(b => b.Genre)
				.WithMany()
				.OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<UserProfile>()
				.HasOne(u => u.Contact)
				.WithOne()
				.OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<LoginDetails>()
				.HasIndex(l => l.Login)
				.IsUnique();
		}
	}
}