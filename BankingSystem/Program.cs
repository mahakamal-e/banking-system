var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
SavingsAccount savings = new SavingsAccount(5);
savings.Deposit(1000);
savings.ApplyInterest();
Console.WriteLine($"Savings Balance after interest: {savings.Balance}");

CheckingAccount checking = new CheckingAccount(500);
checking.Deposit(300);
checking.Withdraw(700);
Console.WriteLine($"Checking Balance after withdrawal: {checking.Balance}");

app.UseAuthorization();

app.MapControllers();

app.Run();
