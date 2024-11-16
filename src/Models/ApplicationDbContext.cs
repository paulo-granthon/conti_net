using Microsoft.EntityFrameworkCore;

namespace ContiNet.Models
{
  public class ApplicationDbContext : DbContext
  {

    public required DbSet<Continent> Continents { get; set; }
    public required DbSet<Country> Countries { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Define Country-Continent Relationship
      modelBuilder.Entity<Country>()
        .HasOne(c => c.Continent)
        .WithMany(c => c.Countries)
        .HasForeignKey(c => c.ContinentId)
        .OnDelete(DeleteBehavior.Restrict);

      // Unique constraint on Continent.Name
      modelBuilder.Entity<Continent>()
        .HasIndex(c => c.Name)
        .IsUnique();
    }
  }
}

