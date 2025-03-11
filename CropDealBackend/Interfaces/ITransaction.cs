using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface ITransactions
    {
        // Get all transactions
        Task<IEnumerable<Transaction>> GetAllTransactions();

        // Get a specific transaction by its ID
        Task<Transaction> GetTransactionById(int transactionId);

        // Get all transactions for a specific dealer
        Task<IEnumerable<Transaction>> GetTransactionsByDealerId(int dealerId);

        // Get all transactions for a specific farmer
        Task<IEnumerable<Transaction>> GetTransactionsByFarmerId(int farmerId);

        // Get transactions based on a date range (filtering)
        Task<IEnumerable<Transaction>> GetTransactionsByDateRange(DateTime startDate, DateTime endDate);

        // Get transactions by transaction mode (e.g., 'Net Banking', 'Debit', 'UPI', etc.)
        Task<IEnumerable<Transaction>> GetTransactionsByMode(string transactionMode);

        // Create a new transaction (returns true if successful)
        Task<bool> CreateTransaction(TransactionVM transaction);

        // Update an existing transaction (optional: return updated transaction or boolean)
        Task<bool> UpdateTransaction(TransactionVM transaction);

        // Delete a transaction by its ID
        Task<bool> DeleteTransaction(int transactionId);

        // Get the total amount of transactions (for reporting or statistics)
        Task<decimal> GetTotalTransactionAmount();

        // Get the total amount of transactions for a specific dealer
        Task<decimal> GetTotalTransactionAmountForDealer(int dealerId);

        // Get the total amount of transactions for a specific farmer
        Task<decimal> GetTotalTransactionAmountForFarmer(int farmerId);

        // Check if a transaction exists (used for validation purposes)
        Task<bool> TransactionExists(int transactionId);

    }
}
