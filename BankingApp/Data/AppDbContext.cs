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
        public DbSet<Account> Accounts { get; set; }
        public DbSet<SalaryDisbursement> SalaryDisbursements { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------------------- ENUM CONFIGURATIONS --------------------
            modelBuilder.Entity<User>()
                .Property(u => u.UserRole)
                .HasConversion<string>();

            modelBuilder.Entity<Client>()
                .Property(c => c.VerificationStatus)
                .HasConversion<string>();

            modelBuilder.Entity<Client>()
                .Property(c => c.AccountType)
                .HasConversion<string>();

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentStatus)
                .HasConversion<string>()
                .HasDefaultValue(PaymentStatus.Pending);

            modelBuilder.Entity<Document>()
                .Property(d => d.DocumentType)
                .HasConversion<string>();

            modelBuilder.Entity<Document>()
                .Property(d => d.DocumentStatus)
                .HasConversion<string>();

            modelBuilder.Entity<Report>()
                .Property(r => r.ReportType)
                .HasConversion<string>();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionType)
                .HasConversion<string>();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionStatus)
                .HasConversion<string>();

            modelBuilder.Entity<SalaryDisbursement>()
                .Property(s => s.Status)
                .HasConversion<string>();

            modelBuilder.Entity<SalaryDisbursement>()
                .Property(s => s.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            // -------------------- RELATIONSHIPS --------------------

            // Client ↔ Bank (required)
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Bank)
                .WithMany(b => b.Clients)
                .HasForeignKey(c => c.BankId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // prevent accidental bank deletion

            // Client ↔ User (required)
           
            // Beneficiary ↔ Client
            modelBuilder.Entity<Beneficiary>()
                .HasOne(b => b.Client)
                .WithMany(c => c.Beneficiaries)
                .HasForeignKey(b => b.ClientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Employee ↔ Client
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Client)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.ClientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Payment ↔ Client
            modelBuilder.Entity<Payment>()
     .HasOne(p => p.Client)
     .WithMany(c => c.Payments)
     .HasForeignKey(p => p.ClientId)
     .OnDelete(DeleteBehavior.Cascade);  // keep cascade here

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Beneficiary)
                .WithMany()
                .HasForeignKey(p => p.BeneficiaryId)
                .OnDelete(DeleteBehavior.Restrict); // 👈 prevent multiple cascade path

            // Document ↔ User
            modelBuilder.Entity<Document>()
                .HasOne(d => d.UploadedBy)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UploadedByUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Report ↔ User
            modelBuilder.Entity<Report>()
                .HasOne(r => r.GeneratedBy)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.GeneratedByUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // SalaryDisbursement ↔ Employee
            modelBuilder.Entity<SalaryDisbursement>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.SalaryDisbursements)
                .HasForeignKey(s => s.EmployeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Transaction ↔ Account
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}

