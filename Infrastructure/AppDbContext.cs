using Core;
using Core.Entities;
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
    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(ConfigureUser);
        modelBuilder.Entity<Employee>(ConfigureEmployee);
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
    
    private static void ConfigureEmployee(EntityTypeBuilder<Employee> builder)
    {
        // Primary key
        builder.HasKey(u => u.EmployeeId);

        // Username
        builder.Property(u => u.AccountId)
            .IsRequired();

        // Email
        builder.Property(u => u.EmailId)
            .IsRequired();

        // PasswordHash
        builder.Property(u => u.Name)
            .HasMaxLength(255)
            .IsRequired();

        // CreatedAt
        builder.Property(u => u.Role)
            .HasMaxLength(255)
            .IsRequired();
    }
}
