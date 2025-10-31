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
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Email> Emails => Set<Email>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<JobAssignment> JobAssignments => Set<JobAssignment>();
    public DbSet<JobInvoice> JobInvoices => Set<JobInvoice>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Phone> Phones => Set<Phone>();
    public DbSet<QuoteRequest> QuoteRequests => Set<QuoteRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(ConfigureUser);
        modelBuilder.Entity<Address>(ConfigureAddress);
        modelBuilder.Entity<Customer>(ConfigureCustomer);
        modelBuilder.Entity<Employee>(ConfigureEmployee);
        modelBuilder.Entity<Invoice>(ConfigureInvoice);
        modelBuilder.Entity<InvoiceItem>(ConfigureInvoiceItem);
        modelBuilder.Entity<Job>(ConfigureJob);
        modelBuilder.Entity<JobAssignment>(ConfigureJobAssignment);
        modelBuilder.Entity<JobInvoice>(ConfigureJobInvoice);
        modelBuilder.Entity<Payment>(ConfigurePayment);
        modelBuilder.Entity<Phone>(ConfigurePhone);
        modelBuilder.Entity<QuoteRequest>(ConfigureQuoteRequest);
    }

    private static void ConfigureCustomer(EntityTypeBuilder<Customer> builder)
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
    
    private static void ConfigureUser(EntityTypeBuilder<User> builder)
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

    private static void ConfigureAddress(EntityTypeBuilder<Address> builder)
    {
        // Primary key
        builder.HasKey(a => a.AddressId);

        // Customer foreign key
        builder.HasOne(a => a.Customer)           // each Address has one Customer
               .WithMany(c => c.Addresses)        // a Customer can have many Addresses
               .HasForeignKey(a => a.CustomerId)  // the FK column on Address
               .OnDelete(DeleteBehavior.Cascade); // cascade delete

        // Street
        builder.Property(a => a.Street)
               .HasMaxLength(255)
               .IsRequired();

        // City
        builder.Property(a => a.City)
               .HasMaxLength(255)
               .IsRequired();

        // State
        builder.Property(a => a.State)
               .HasMaxLength(255)
               .IsRequired();

        // PostalCode
        builder.Property(a => a.PostalCode)
                .IsRequired();

        // Country
        builder.Property(a => a.Country)
                .IsRequired(false);

        // Address Type
        builder.Property(a => a.AddressType)
               .HasMaxLength(255)
               .IsRequired();
    }
    
    private static void ConfigureInvoice(EntityTypeBuilder<Invoice> builder)
    {
           builder.HasKey(a => a.InvoiceId);
           
           builder.HasMany(a => a.JobInvoices)
                  .WithOne(c => c.Invoice)
                  .HasForeignKey(a => a.InvoiceId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.Property(a => a.DateIssued);
           
           builder.Property(a => a.Method)
                  .HasMaxLength(100);
           
           builder.Property(a => a.SellerDetails)
                  .HasMaxLength(255);
    }
    
    private static void ConfigureInvoiceItem(EntityTypeBuilder<InvoiceItem> builder)
    {
           builder.HasKey(a => a.ItemId);
           
           builder.HasOne(a => a.Invoice)
                  .WithMany(c => c.InvoiceItems)
                  .HasForeignKey(a => a.InvoiceId)
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.Property(a => a.Description)
                  .HasMaxLength(255);

           builder.Property(a => a.Amount);
           
           builder.Property(a => a.Price);
    }
    
    private static void ConfigureJob(EntityTypeBuilder<Job> builder)
    {
           builder.HasKey(a => a.JobId);
           
           builder.HasMany(a => a.JobInvoices)
                  .WithOne(c => c.Job)
                  .HasForeignKey(a => a.JobId)
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.HasMany(a => a.JobAssignments)
                  .WithOne(c => c.Job)
                  .HasForeignKey(a => a.JobId)
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.HasMany(a => a.QuoteRequests)
                  .WithOne(c => c.Job)
                  .HasForeignKey(a => a.JobId)
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.HasOne(a => a.Customer)
                  .WithMany(c => c.Jobs)
                  .HasForeignKey(a => a.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.Property(a => a.JobDescription)
                  .HasMaxLength(255);

           builder.Property(a => a.StartDate);
           
           builder.Property(a => a.EndDate);
           
           builder.Property(a => a.Status)
                  .HasMaxLength(100);
    }
    
    private static void ConfigureJobAssignment(EntityTypeBuilder<JobAssignment> builder)
    {
           builder.HasKey(a => new {a.JobId, a.UserId});
           
           builder.HasOne(a => a.Job)
                  .WithMany(c => c.JobAssignments)
                  .HasForeignKey(a => a.JobId)
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.HasOne(a => a.User)
                  .WithMany(c => c.JobAssignments)
                  .HasForeignKey(a => a.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.Property(a => a.AssignmentDate);
           
           builder.Property(a => a.RoleOnJob)
                  .HasMaxLength(100);
    }
    
    private static void ConfigureJobInvoice(EntityTypeBuilder<JobInvoice> builder)
    {
           builder.HasKey(a => new {a.JobId, a.InvoiceId});
           
           builder.HasOne(a => a.Job)
                  .WithMany(c => c.JobInvoices)
                  .HasForeignKey(a => a.JobId)
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.HasOne(a => a.Invoice)
                  .WithMany(c => c.JobInvoices)
                  .HasForeignKey(a => a.InvoiceId)
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.Property(a => a.CreatedDate);

           builder.Property(a => a.PercentageOfInvoice);
    }
    
    private static void ConfigurePayment(EntityTypeBuilder<Payment> builder)
    {
           builder.HasKey(a => a.PaymentId);
           
           builder.HasOne(a => a.Invoice)
                  .WithMany(c => c.Payments)
                  .HasForeignKey(a => a.InvoiceId)
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.Property(a => a.PaymentDate);
           
           builder.Property(a => a.AmountPaid);
           
           builder.Property(a => a.Method)
                  .HasMaxLength(100);
    }
    
    private static void ConfigurePhone(EntityTypeBuilder<Phone> builder)
    {
           builder.HasKey(a => a.PhoneId);
           
           builder.HasOne(a => a.Customer)
                  .WithMany(c => c.Phones)
                  .HasForeignKey(a => a.Customer)
                  .OnDelete(DeleteBehavior.Cascade);
           
           builder.Property(a => a.PhoneNumber)
                  .HasMaxLength(100);
           
           builder.Property(a => a.PhoneType)
                  .HasMaxLength(100);
    }
    
    private static void ConfigureQuoteRequest(EntityTypeBuilder<QuoteRequest> builder)
    {
           builder.HasKey(a => a.QuoteId);
           
           builder.HasOne(a => a.Job)
                  .WithMany(c => c.QuoteRequests)
                  .HasForeignKey(a => a.JobId)
                  .OnDelete(DeleteBehavior.Cascade);

           builder.Property(a => a.RequestDate);
           
           builder.Property(a => a.DescriptionOfWork)
                  .HasMaxLength(255);
           
           builder.Property(a => a.EstimatedPrice);
    }
}
