using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.EntityFrameworkCore;
namespace CropDealBackend.Repositories
{
    public class TransactionsRepository : ITransactions
    {
        private readonly CropDealContext _context;
        public TransactionsRepository(CropDealContext context)
        {
            _context = context;
        }

        // Get all transactions
        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        // Get a specific transaction by its ID
        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == transactionId);
        }

        // Get all transactions for a specific dealer
        public async Task<IEnumerable<Transaction>> GetTransactionsByDealerId(int dealerId)
        {
            return await _context.Transactions.Where(t => t.DealerId == dealerId)
                .ToListAsync();
        }

        // Get all transactions for a specific farmer
        public async Task<IEnumerable<Transaction>> GetTransactionsByFarmerId(int farmerId)
        {
            return await _context.Transactions.Where(t => t.FarmerId == farmerId)
                .ToListAsync();
        }

        // Get transactions based on a date range
        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Transactions
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .ToListAsync();
        }

        // Get transactions by transaction mode
        public async Task<IEnumerable<Transaction>> GetTransactionsByMode(string transactionMode)
        {
            return await _context.Transactions
                .Where(t => t.TransactionMode == transactionMode)
                .ToListAsync();
        }

        // Create a new transaction
        public async Task<bool> CreateTransaction(TransactionVM transaction)
        {
            var newTransaction = new Transaction
            {
                TransactionDate = transaction.TransactionDate,
                DealerId = transaction.DealerId,
                FarmerId = transaction.FarmerId,
                TransactionMode = transaction.TransactionMode,
                FarmerBankAccId = transaction.FarmerBankAccId,
                DealerBankAccId = transaction.DealerBankAccId,
                InvoiceId = transaction.InvoiceId
            };

            _context.Transactions.Add(newTransaction);
            return await _context.SaveChangesAsync() > 0;
        }

        // Update an existing transaction
        public async Task<bool> UpdateTransaction(TransactionVM transaction)
        {
            var existingTransaction = await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == transaction.TransactionId);

            if (existingTransaction == null) return false;

            existingTransaction.TransactionDate = transaction.TransactionDate;
            existingTransaction.DealerId = transaction.DealerId;
            existingTransaction.FarmerId = transaction.FarmerId;
            existingTransaction.TransactionMode = transaction.TransactionMode;
            existingTransaction.FarmerBankAccId = transaction.FarmerBankAccId;
            existingTransaction.DealerBankAccId = transaction.DealerBankAccId;
            existingTransaction.InvoiceId = transaction.InvoiceId;

            return await _context.SaveChangesAsync() > 0;
        }

        // Delete a transaction by its ID
        public async Task<bool> DeleteTransaction(int transactionId)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == transactionId);
            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            return await _context.SaveChangesAsync() > 0;
        }

        // Get the total amount of transactions
        public async Task<decimal> GetTotalTransactionAmount()
        {
            return (decimal)await _context.Transactions
                .SumAsync(t => t.Invoice != null ? t.Invoice.TotalAmount : 0);
        }

        // Get the total amount of transactions for a specific dealer
        public async Task<decimal> GetTotalTransactionAmountForDealer(int dealerId)
        {
            return (decimal)await _context.Transactions
                .Where(t => t.DealerId == dealerId)
                .SumAsync(t => t.Invoice != null ? t.Invoice.TotalAmount : 0);
        }

        // Get the total amount of transactions for a specific farmer
        public async Task<decimal> GetTotalTransactionAmountForFarmer(int farmerId)
        {
            return (decimal)await _context.Transactions
                .Where(t => t.FarmerId == farmerId)
                .SumAsync(t => t.Invoice != null ? t.Invoice.TotalAmount : 0);
        }

        // Check if a transaction exists
        public async Task<bool> TransactionExists(int transactionId)
        {
            return await _context.Transactions.AnyAsync(t => t.TransactionId == transactionId);
        }
    }
}