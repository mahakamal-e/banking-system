public class Transaction
{
    public int TransactionId { get; set; }
    public int AccountId { get; set; } //foreign key to Account
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public string Type { get; set; } // "Deposit", "Withdraw", "Transfer"
    
    public Account Account { get; set; } // Navigation property for Account
}