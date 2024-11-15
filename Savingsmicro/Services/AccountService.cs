using Microsoft.EntityFrameworkCore;
using Savingsmicro.Data;
using Savingsmicro.Model;

namespace Savingsmicro.Services
{
    public class AccountService:IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAccountAsync(string userId, string accountType)
        {
            var account = new Account
            {
                UserId = userId,
                AccountType = accountType,
                Balance = 0,
                CreatedAt = DateTime.UtcNow,
                Transactions = new List<Transaction>()
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetAccountAsync(int accountId)
        {
            return await _context.Accounts
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.Id == accountId);
        }

        public async Task<decimal> GetBalanceAsync(int accountId)
        {
            var account = await GetAccountAsync(accountId);
            return account?.Balance ?? 0;
        }

        public async Task<List<Account>> GetUserAccountsAsync(string userId)
        {
            return await _context.Accounts
                .Include(a => a.Transactions)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }
    }
}
