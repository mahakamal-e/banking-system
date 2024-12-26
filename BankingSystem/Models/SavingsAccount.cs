
// SavingsAccount class inherits from Account
public class SavingsAccount : Account
{
    public decimal InterestRate { get; set; } // Annual interest rate in percentage

    // Constructor to initialize the interest rate
    public SavingsAccount(decimal interestRate)
    {
        InterestRate = interestRate;
    }

    // Override Withdraw method for savings account rules
    public override void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }
        Balance -= amount;
    }

    // Method to apply interest to the account balance
    public void ApplyInterest()
    {
        decimal interest = Balance * (InterestRate / 100);
        Balance += interest;
    }
}

