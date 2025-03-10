using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface IBankDetail
    {
        Task<IEnumerable<BankDetail>> GetAllBankDetails();
        Task<BankDetail> GetBankDetailById(int bankId);
        Task<BankDetail> GetBankDetailByUserId(int userId);
        Task<bool> AddBankDetail(BankDetail bankDetail);
        Task<bool> UpdateBankDetail(BankDetail bankDetail);
        Task<bool> DeleteBankDetail(int bankId);

        // Retrieve all bank details for a given user (in case of multiple accounts)
        Task<IEnumerable<BankDetail>> GetAllBankDetailsByUserId(int userId);

        // Get bank details by account number
        Task<BankDetail> GetBankDetailByAccountNumber(string accountNumber);

        // Check if a user already has a bank account
        //Task<bool> UserHasBankAccount(int userId);

        // Retrieve the total number of bank accounts in the system
        Task<int> GetTotalBankAccounts();

        // Retrieve recently added bank accounts (for admin review)
        Task<IEnumerable<BankDetail>> GetRecentBankDetails(int count);

        // Get bank details by bank name
        Task<IEnumerable<BankDetail>> GetBankDetailsByBankName(string bankName);

        // Retrieve distinct bank names available in the system
        Task<IEnumerable<string>> GetDistinctBankNames();
    }
}
