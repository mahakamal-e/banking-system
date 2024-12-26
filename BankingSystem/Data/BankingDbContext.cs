using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models
{
    public class BankingDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        // Constructor with options parameter
        public BankingDbContext(DbContextOptions<BankingDbContext> options)
            : base(options)
        {
        }

        // Configure relationships and table mappings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table-per-Hierarchy (TPH) mapping for Account inheritance
            modelBuilder.Entity<Account>()
                .HasDiscriminator<string>("AccountType")  // Discriminator column to differentiate account types
                .HasValue<CheckingAccount>("Checking")    // Checking account type
                .HasValue<SavingsAccount>("Savings");     // Savings account type

            // Relationship between Account and Transaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account) // Transaction has a reference to Account
                .WithMany(a => a.Transactions) // Account can have many Transactions
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: cascading delete when account is deleted
        }
    }
}


