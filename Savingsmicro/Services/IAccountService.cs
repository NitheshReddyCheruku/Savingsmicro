using Savingsmicro.Model;

namespace Savingsmicro.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(string userId, string accountType);
        Task<Account> GetAccountAsync(int accountId);
        Task<decimal> GetBalanceAsync(int accountId);
        Task<List<Account>> GetUserAccountsAsync(string userId);
    }
}
