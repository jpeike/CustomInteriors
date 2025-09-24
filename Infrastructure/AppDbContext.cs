using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Email> Emails => Set<Email>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(ConfigureUser);
        modelBuilder.Entity<Email>(ConfigureEmail);
    }

    private void ConfigureUser(EntityTypeBuilder<User> builder)
    {
        // Primary key
        builder.HasKey(u => u.Id);

        // Username
        builder.Property(u => u.Username)
               .HasMaxLength(100)
               .IsRequired();

        // Email
        builder.Property(u => u.Email)
               .HasMaxLength(255)
               .IsRequired();

        // PasswordHash 
        builder.Property(u => u.PasswordHash)
               .HasMaxLength(512)
               .IsRequired();

        // CreatedAt
        builder.Property(u => u.CreatedOn)
               .HasDefaultValueSql("GETDATE()")
               .IsRequired();

        // Optional: Index on Username or Email
        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
    }

    private void ConfigureEmail(EntityTypeBuilder<Email> builder)
    {
        // Primary key
        builder.HasKey(e => e.EmailID);

        // CustomerID (FK)
        builder.Property(e => e.CustomerID)
               .IsRequired();

        builder.HasOne<Customer>()                  // assumes you have a Customer entity
               .WithMany()                          // one Customer -> many Emails
               .HasForeignKey(e => e.CustomerID)
               .OnDelete(DeleteBehavior.Cascade); 

        // EmailAddress
        builder.Property(e => e.EmailAddress)
               .HasMaxLength(255)
               .IsRequired();

        // EmailType
        builder.Property(e => e.EmailType)
               .HasMaxLength(100)
               .IsRequired();

        // CreatedOn
        builder.Property(e => e.CreatedOn)
               .HasDefaultValueSql("GETDATE()")
               .IsRequired();

        // Unique index to prevent duplicate email addresses for the same customer
        builder.HasIndex(e => new { e.CustomerID, e.EmailAddress })
               .IsUnique();
    }

}
