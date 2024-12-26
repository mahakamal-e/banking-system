// abstract class for shared implementation & custom implementation for another methods.
public abstract class Account
{
    //The value of account balance is stored as a decimal to ensure precision and accuracy.
private decimal _balance;
public decimal Balance
{
    get { return _balance; }
    protected set { _balance = value; }
}
public int AccountNumber {get; set;}
//virtual keyword to mark it as can be override
public virtual void Deposit(decimal amount)
{
    if (amount <= 0)
    {
            throw new ArgumentException("");
    }
    Balance += amount;
    }

}

 public abstract void Withdraw(decimal amount);