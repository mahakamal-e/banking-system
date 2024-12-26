// CheckingAccount class inherits from Account
public class CheckingAccount : Account
{
    public decimal AllowedOverdraft { get; set; }

    // Constructor to initialize the overdraft limit
    public CheckingAccount(decimal allowedOverdraft = 500)
    {
        AllowedOverdraft = allowedOverdraft;
    }

    // Override Withdraw method for checking account rules
    public override void Withdraw(decimal amount)
    {
        if (amount > Balance + AllowedOverdraft)
        {
            throw new InvalidOperationException("You exceeded the overdraft limit.");
        }
        Balance -= amount;
    }
}
