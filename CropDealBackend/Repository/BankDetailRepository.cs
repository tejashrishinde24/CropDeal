using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDealBackend.Repository
{
    public class BankDetailRepository:IBankDetail
    {
        private CropDealContext _context;
        public BankDetailRepository(CropDealContext context)
        {
            _context = context;
        }

        public async Task<bool> AddBankDetail(BankDetailVM bankObj)
        {
            var res = await _context.BankDetails.FirstOrDefaultAsync(x => x.Id == bankObj.Id);
            if (res != null) return false;
            BankDetail bankDetail = new BankDetail()
            {
                Id = bankObj.Id,
                AccountHolderName = bankObj.AccountHolderName,
                BankName = bankObj.BankName,
                BankAccountNumber = bankObj.BankAccountNumber,
                Ifsccode = bankObj.Ifsccode,
                UserId = bankObj.UserId,


            };
            await _context.BankDetails.AddAsync(bankDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBankDetail(int bankId)
        {
            var bankDetail = await _context.BankDetails.FindAsync(bankId);
            if (bankDetail == null)
                return false;

            _context.BankDetails.Remove(bankDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BankDetail>> GetAllBankDetails()
        {
            return await _context.BankDetails.ToListAsync();
        }

        public async Task<IEnumerable<BankDetail>> GetAllBankDetailsByUserId(int userId)
        {
            return await _context.BankDetails.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<BankDetail> GetBankDetailByAccountNumber(string accountNumber)
        {
            return await _context.BankDetails.FirstOrDefaultAsync(b => b.BankAccountNumber == accountNumber);
        }

        public async Task<BankDetail> GetBankDetailById(int bankId)
        {
            return await _context.BankDetails.FindAsync(bankId);

        }

        public async Task<BankDetail> GetBankDetailByUserId(int userId)
        {
            return await _context.BankDetails.FirstOrDefaultAsync(b => b.UserId == userId);
        }

        public async Task<IEnumerable<BankDetail>> GetBankDetailsByBankName(string bankName)
        {
            return await _context.BankDetails.Where(b => b.BankName == bankName).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetDistinctBankNames()
        {
            return await _context.BankDetails.Select(b => b.BankName).Distinct().ToListAsync();
        }



        public async Task<int> GetTotalBankAccounts()
        {
            return await _context.BankDetails.CountAsync();
        }

        public async Task<bool> UpdateBankDetail(BankDetailVM bankObj)
        {
            var res = await _context.BankDetails.FirstOrDefaultAsync(x => x.Id == bankObj.Id);
            if (res == null) return false;
            BankDetail bankDetail = new BankDetail()
            {
                Id = bankObj.Id,
                AccountHolderName = bankObj.AccountHolderName,
                BankName = bankObj.BankName,
                BankAccountNumber = bankObj.BankAccountNumber,
                Ifsccode = bankObj.Ifsccode,
                UserId = bankObj.UserId,


            };

            _context.BankDetails.Update(bankDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserHasBankAccount(int userId)
        {
            return await _context.BankDetails.AnyAsync(b => b.UserId == userId);
        }
    }
}
