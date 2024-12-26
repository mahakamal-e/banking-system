using Microsoft.EntityFrameworkCore;
using BankingSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddDbContext<BankingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add other services like Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Create a scope to resolve DbContext in Main method
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BankingDbContext>(); // Resolve DbContext from the scope

    // Ensure the database is created
    context.Database.EnsureCreated();

    // Create and save a new SavingsAccount
    var savingsAccount = new SavingsAccount(5);
    savingsAccount.Deposit(1000); // Use Deposit method to set initial balance
    Console.WriteLine($"Savings Account Balance: {savingsAccount.Balance}");

    context.Accounts.Add(savingsAccount);
    context.SaveChanges();
    Console.WriteLine("Savings Account saved to the database.");

    // Create and save a Transaction for the SavingsAccount
    var transaction = new Transaction
    {
        AccountId = savingsAccount.AccountId,
        Amount = 500,
        Type = "Deposit",
        Timestamp = DateTime.UtcNow
    };

    context.Transactions.Add(transaction);
    context.SaveChanges();
    Console.WriteLine("Transaction saved to the database.");

    // Retrieve and display all accounts and their transactions
    var accounts = context.Accounts.Include(a => a.Transactions).ToList();
    Console.WriteLine("\nAccounts and their transactions:");
    foreach (var account in accounts)
    {
        Console.WriteLine($"Account {account.AccountId} Balance: {account.Balance}");
        foreach (var trans in account.Transactions)
        {
            Console.WriteLine($"- Transaction: {trans.Type}, Amount: {trans.Amount}, Timestamp: {trans.Timestamp}");
        }
    }
}


app.UseAuthorization();
app.MapControllers();
app.Run();
