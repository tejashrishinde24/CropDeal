using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface IBankDetail
    {
        // Retrieve all bank details
        Task<IEnumerable<BankDetail>> GetAllBankDetails();

        // Retrieve a specific bank detail by its ID
        Task<BankDetail> GetBankDetailById(int bankId);

        // Retrieve bank details for a specific user
        Task<BankDetail> GetBankDetailByUserId(int userId);

        // Retrieve all bank details for a given user (in case of multiple accounts)
        Task<IEnumerable<BankDetail>> GetAllBankDetailsByUserId(int userId);

        // Get bank details by account number
        Task<BankDetail> GetBankDetailByAccountNumber(string accountNumber);

        // Add a new bank detail
        Task<bool> AddBankDetail(BankDetailVM bankDetail);

        // Update an existing bank detail
        Task<bool> UpdateBankDetail(BankDetailVM bankDetail);


        // Delete a bank detail by ID
        Task<bool> DeleteBankDetail(int bankId);

        // Check if a user already has a bank account
        Task<bool> UserHasBankAccount(int userId);

        // Retrieve the total number of bank accounts in the system
        Task<int> GetTotalBankAccounts();

        // Retrieve recently added bank accounts (for admin review)
        // Task<IEnumerable<BankDetail>> GetRecentBankDetails(int count);

        // Get bank details by bank name
        Task<IEnumerable<BankDetail>> GetBankDetailsByBankName(string bankName);

        // Retrieve distinct bank names available in the system
        Task<IEnumerable<string>> GetDistinctBankNames();
    }
}
