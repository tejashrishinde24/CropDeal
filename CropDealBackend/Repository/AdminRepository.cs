using CropDealBackend.Models;

namespace CropDealBackend.Repository
{
    public class AdminRepository
    {
        private CropDealContext _context;
        public AdminRepository(CropDealContext context)
        {
            _context = context;
        }
    }
}
