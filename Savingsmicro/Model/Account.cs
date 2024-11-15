namespace Savingsmicro.Model
{
    public class Account
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AccountType { get; set; } // Savings or Investment
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
