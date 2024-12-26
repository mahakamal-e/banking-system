public class CheckingAccount : Account
{
    public decimal AllowedOverdraft { get; set; }

    // Constructor to initialize the overdraft limit.
    public CheckingAccount(decimal allowedOverdraft = 500)
    {
        this.AllowedOverdraft = allowedOverdraft;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount > Balance + AllowedOverdraft)
        {
            throw new InvalidOperationException("You exceeded the limit.");
        }
        
        Balance -= amount;  // Subtract the amount from Balance
    }
}
