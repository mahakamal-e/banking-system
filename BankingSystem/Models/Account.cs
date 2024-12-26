// Abstract class for shared implementation & custom behavior
public abstract class Account
{
    public int AccountId { get; set; } // shared  primary key
    public int AccountNumber { get; set; } 
    // The value of account balance is stored as a decimal to ensure precision and accuracy.
    
    
    private decimal _balance;

    // Account balance property with getter and protected setter
    public decimal Balance
    {
        get { return _balance; }
        protected set { _balance = value; }
    }

    
    public virtual ICollection<Transaction> Transactions { get; set; }
    // Virtual deposit method, allowing override if needed
    public virtual void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive.");
        }
        Balance += amount;
    }

    // Abstract withdraw method to enforce implementation in derived classes
    public abstract void Withdraw(decimal amount);
}