using System.Transactions;

namespace CropDealBackend.Interfaces
{
    public interface ITransaction
    {  
        Task<IEnumerable<Transaction>> GetAllTransactions();
        Task<Transaction> GetTransactionById(int transactionId);
        Task<IEnumerable<Transaction>> GetTransactionsByFarmerId(int userId);
        Task<IEnumerable<Transaction>> GetTransactionsByDealerId(int userId);
        Task<bool> AddTransaction(Transaction transaction);
        Task<bool> UpdateTransaction(Transaction transaction);
        Task<bool> DeleteTransaction(int transactionId);
        Task<bool> TransactionExists(int transactionId);
        Task<string> GetTransactionStatus(int transactionId);
        // Get transactions based on a date range (filtering)
        Task<IEnumerable<Transaction>> GetTransactionsByDateRange(DateTime startDate, DateTime endDate);

        // Get transactions by transaction mode (e.g., 'Net Banking', 'Debit', 'UPI', etc.)
        Task<IEnumerable<Transaction>> GetTransactionsByMode(string transactionMode);

        // Create a new transaction (returns true if successful)
        Task<bool> CreateTransaction(Transaction transaction);

        // Get the total amount of transactions (for reporting or statistics)
        Task<decimal> GetTotalTransactionAmount();

        // Get the total amount of transactions for a specific dealer
        Task<decimal> GetTotalTransactionAmountForDealer(int dealerId);

        // Get the total amount of transactions for a specific farmer
        Task<decimal> GetTotalTransactionAmountForFarmer(int farmerId);

    }
}
