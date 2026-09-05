using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

/// <summary>
/// Application database context.
/// Provides access to the database entities and configures model mappings.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Represents the Contacts table in the database.
    /// </summary>
    public DbSet<Contact> Contacts { get; set; }

    /// <summary>
    /// Configures entity mappings and conversions.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure entities.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Store ContactType enum as string
        modelBuilder.Entity<Contact>()
            .Property(c => c.ContactType)
            .HasConversion<string>();

        // Store Status enum as string
        modelBuilder.Entity<Contact>()
            .Property(c => c.Status)
            .HasConversion<string>();
    }
}
