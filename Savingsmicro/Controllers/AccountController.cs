using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavingsInvestmentService.Services;
using Savingsmicro.Model;
using Savingsmicro.Services;

namespace Savingsmicro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public AccountController(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount(string userId, string accountType)
        {
            var account = await _accountService.CreateAccountAsync(userId, accountType);
            return Ok(account);
        }

        [HttpGet("{accountId}")]
        public async Task<ActionResult<Account>> GetAccount(int accountId)
        {
            var account = await _accountService.GetAccountAsync(accountId);
            if (account == null)
                return NotFound();
            return Ok(account);
        }

        [HttpGet("{accountId}/balance")]
        public async Task<ActionResult<decimal>> GetBalance(int accountId)
        {
            var balance = await _accountService.GetBalanceAsync(accountId);
            return Ok(balance);
        }

        [HttpPost("{accountId}/transaction")]
        public async Task<ActionResult<Transaction>> CreateTransaction(int accountId, string type, decimal amount)
        {
            try
            {
                var transaction = await _transactionService.CreateTransactionAsync(accountId, type, amount);
                return Ok(transaction);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{accountId}/transactions")]
        public async Task<ActionResult<List<Transaction>>> GetTransactions(int accountId)
        {
            var transactions = await _transactionService.GetAccountTransactionsAsync(accountId);
            return Ok(transactions);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Account>>> GetUserAccounts(string userId)
        {
            var accounts = await _accountService.GetUserAccountsAsync(userId);
            return Ok(accounts);
        }
    }
}
