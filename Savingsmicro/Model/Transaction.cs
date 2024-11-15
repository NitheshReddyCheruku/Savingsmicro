namespace Savingsmicro.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; } // Deposit, Withdrawal, Investment
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public Account Account { get; set; }
    }
}
