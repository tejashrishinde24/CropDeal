using CropDealBackend.Models;

namespace CropDealBackend.Repository
{
    public class InvoiceRepository
    {
        private CropDealContext _context;
        public InvoiceRepository(CropDealContext context)
        {
            _context = context;
        }
    }
}
