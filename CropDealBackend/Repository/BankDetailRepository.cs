using CropDealBackend.Models;

namespace CropDealBackend.Repository
{
    public class BankDetailRepository
    {
        private CropDealContext _context;
        public BankDetailRepository(CropDealContext context)
        {
            _context = context;
        }
    }
}
