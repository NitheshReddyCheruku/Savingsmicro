
using Savingsmicro.Model;


namespace SavingsInvestmentService.Services
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransactionAsync(int accountId, string type, decimal amount);
        Task<List<Transaction>> GetAccountTransactionsAsync(int accountId);
    }
}