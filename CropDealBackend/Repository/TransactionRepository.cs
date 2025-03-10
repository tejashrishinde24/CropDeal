using CropDealBackend.Models;

namespace CropDealBackend.Repository
{
    public class TransactionRepository
    {
        private CropDealContext _context;
        public TransactionRepository(CropDealContext context)
        {
            _context = context;
        }
    }
}
