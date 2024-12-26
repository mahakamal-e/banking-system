// Create controller to handel HTTP requests like deposits, withdrawals and transaction history

using BankingSystem.Models; // containes Account,CheckingAccount ...
using Microsoft.AspNetCore.Mvc; // This is ASP.NET library building APIs & handling http requests
using Microsoft.EntityFrameworkCore; //This is library for database operstions
using System;
using System.Linq;
using System.Threading.Tasks; //System library handling async & queires

namespace BankingSystem.Controllers
{
    // the route api/accounts/ and so on after that ex. api/accounts/deposit
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly BankingDbContext _context;

        public AccountsController(BankingDbContext context)
        {
            _context = context;
        }

        // POST: api/accounts/deposit
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionRequest request)
        {
            var account = await _context.Accounts.FindAsync(request.AccountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            try
            {
                account.Deposit(request.Amount);
                _context.Transactions.Add(new Transaction
                {
                    AccountId = account.AccountId,
                    Amount = request.Amount,
                    Timestamp = DateTime.UtcNow,
                    Type = "Deposit"
                });

                await _context.SaveChangesAsync();
                return Ok(new { message = "Deposit successful", balance = account.Balance });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/accounts/withdraw
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionRequest request)
        {
            var account = await _context.Accounts.FindAsync(request.AccountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            try
            {
                account.Withdraw(request.Amount);
                _context.Transactions.Add(new Transaction
                {
                    AccountId = account.AccountId,
                    Amount = request.Amount,
                    Timestamp = DateTime.UtcNow,
                    Type = "Withdraw"
                });

                await _context.SaveChangesAsync();
                return Ok(new { message = "Withdrawal successful", balance = account.Balance });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/accounts/transfer
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
        {
            var sourceAccount = await _context.Accounts.FindAsync(request.SourceAccountId);
            var targetAccount = await _context.Accounts.FindAsync(request.TargetAccountId);

            if (sourceAccount == null || targetAccount == null)
            {
                return NotFound("Source or target account not found.");
            }

            if (request.Amount <= 0)
            {
                return BadRequest("Transfer amount must be positive.");
            }

            try
            {
                sourceAccount.Withdraw(request.Amount);
                targetAccount.Deposit(request.Amount);

                _context.Transactions.Add(new Transaction
                {
                    AccountId = sourceAccount.AccountId,
                    Amount = request.Amount,
                    Timestamp = DateTime.UtcNow,
                    Type = "Transfer"
                });

                _context.Transactions.Add(new Transaction
                {
                    AccountId = targetAccount.AccountId,
                    Amount = request.Amount,
                    Timestamp = DateTime.UtcNow,
                    Type = "Transfer"
                });

                await _context.SaveChangesAsync();
                return Ok(new { message = "Transfer successful", sourceBalance = sourceAccount.Balance, targetBalance = targetAccount.Balance });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/accounts/{id}/balance
        [HttpGet("{id}/balance")]
        public async Task<IActionResult> GetBalance(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            return Ok(new { balance = account.Balance });
        }
    }

    public class TransactionRequest
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }

    public class TransferRequest
    {
        public int SourceAccountId { get; set; }
        public int TargetAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}