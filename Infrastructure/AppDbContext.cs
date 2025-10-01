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
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Email> Emails => Set<Email>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(ConfigureUser);
        modelBuilder.Entity<Email>(ConfigureEmail);
        modelBuilder.Entity<Customer>(ConfigureCustomer);
    }

    private void ConfigureCustomer(EntityTypeBuilder<Customer> builder)
    {
        // Primary key
        builder.HasKey(u => u.CustomerId);

        // FirstName
        builder.Property(u => u.FirstName)
               .HasMaxLength(100)
               .IsRequired();

        // LastName
        builder.Property(u => u.LastName)
               .HasMaxLength(100)
               .IsRequired();

        // CustomerType
        builder.Property(u => u.CustomerType)
               .HasMaxLength(100);

        // PrefferedContactMethod
        builder.Property(u => u.PrefferedContactMethod)
               .HasMaxLength(100);

        // CompanyName
        builder.Property(u => u.CompanyName)
               .HasMaxLength(100);

        // Status
        builder.Property(u => u.Status)
               .HasMaxLength(100);

        // Status
        builder.Property(u => u.CustomerNotes);

        // Optional: Index on Username or Email
        builder.HasIndex(u => u.CustomerId).IsUnique();

        // One-to-many: Customer has many Emails
        builder.HasMany(c => c.Emails)
               .WithOne(e => e.Customer)
               .HasForeignKey(e => e.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);
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

        // CustomerId (FK)
        builder.Property(e => e.CustomerId)
               .IsRequired();

        //builder.HasOne<Customer>()                  // assumes you have a Customer entity
        //       .WithMany()                          // one Customer -> many Emails
        //       .HasForeignKey(e => e.CustomerId)
        //       .OnDelete(DeleteBehavior.Cascade); 

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
        builder.HasIndex(e => new { e.CustomerId, e.EmailAddress })
               .IsUnique();
    }

}
