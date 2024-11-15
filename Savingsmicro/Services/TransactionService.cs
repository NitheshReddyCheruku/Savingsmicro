

using Microsoft.EntityFrameworkCore;
using SavingsInvestmentService.Services;
using Savingsmicro.Data;
using Savingsmicro.Model;

namespace Savingsmicro.Services
{
    public class TransactionService:ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateTransactionAsync(int accountId, string type, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
                throw new ArgumentException("Account not found");

            if (type == "Withdrawal" && account.Balance < amount)
                throw new InvalidOperationException("Insufficient funds");

            var transaction = new Transaction
            {
                AccountId = accountId,
                Type = type,
                Amount = amount,
                TransactionDate = DateTime.UtcNow
            };

            account.Balance += type == "Withdrawal" ? -amount : amount;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<List<Transaction>> GetAccountTransactionsAsync(int accountId)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
    }
}
