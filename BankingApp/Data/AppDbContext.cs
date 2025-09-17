using BankingApp.Enums;
using Microsoft.EntityFrameworkCore;


namespace BankingApp.Models
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> options)
            : base(options) { }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Report> Reports { get; set; }

        public DbSet<SalaryDisbursement> SalaryDisbursements { get; set; }
        // If you also have Transactions table
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 UserRole enum stored as string
            modelBuilder.Entity<User>()
                .Property(u => u.UserRole)
                .HasConversion<string>();

            // 🔹 Client Enums
            modelBuilder.Entity<Client>()
                .Property(c => c.VerificationStatus)
                .HasConversion<string>();

            modelBuilder.Entity<Client>()
                .Property(c => c.AccountType)
                .HasConversion<string>();


            // 🔹 Payment Enums
            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentStatus)
                .HasConversion<string>();

            // 🔹 Document Enums
            modelBuilder.Entity<Document>()
                .Property(d => d.DocumentType)
                .HasConversion<string>();

            modelBuilder.Entity<Document>()
                .Property(d => d.DocumentStatus)
                .HasConversion<string>();

            // 🔹 Report Enum
            modelBuilder.Entity<Report>()
                .Property(r => r.ReportType)
                .HasConversion<string>();

            // 🔹 Transaction Enums
            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionType)
                .HasConversion<string>();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionStatus)
                .HasConversion<string>();

            // 🔹 Decimal precision for payment amount
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");


            // SalaryDisbursement
            modelBuilder.Entity<SalaryDisbursement>()
                .Property(s => s.Status)
                .HasConversion<string>();

            modelBuilder.Entity<SalaryDisbursement>()
                .Property(s => s.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SalaryDisbursement>()
                .HasOne(s => s.Employee)
                .WithMany()
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            // 🔹 Relationships
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Bank)
                .WithMany(b => b.Clients)
                .HasForeignKey(c => c.BankId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithMany(u => u.Clients)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Beneficiary>()
                .HasOne(b => b.Client)
                .WithMany(c => c.Beneficiaries)
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Client)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Beneficiary)
                .WithMany()
                .HasForeignKey(p => p.BeneficiaryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.UploadedBy)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UploadedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasOne(r => r.GeneratedBy)
                      .WithMany(u => u.Reports)
                      .HasForeignKey(r => r.GeneratedByUserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
