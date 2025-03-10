using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDealBackend.Repository
{
    public class CropDetailRepository : ICropDetail
    {
        private CropDealContext _context;
        public CropDetailRepository(CropDealContext context)
        {
            _context = context;
        }

        // ✅ Get all crops
        public async Task<IEnumerable<CropDetail>> GetAllCrops()
        {
            return await _context.CropDetails.ToListAsync();
        }

        // ✅ Get crop by ID
        public async Task<CropDetail> GetCropById(int cropId)
        {
            return await _context.CropDetails.FindAsync(cropId);
        }

        // ✅ Get crops by category
        public async Task<IEnumerable<CropDetail>> GetCropsByCategory(int cropTypeId)
        {
            return await _context.CropDetails
        .Where(c => c.CropTypeId == cropTypeId)
        .ToListAsync();
        }

        // ✅ Create a new crop
        public async Task<bool> CreateCropDetail(CropDetailVM cropDetail)
        {
            var crop = new CropDetail
            {
                CropName = cropDetail.CropName,
                CropTypeId = cropDetail.CropTypeId,
                QuantityAvailable = cropDetail.QuantityAvailable,
                Price = cropDetail.Price,
                CropLocation = cropDetail.CropLocation,
                Status = cropDetail.Status,
                FarmerId = cropDetail.FarmerId,
                ImageUrl = cropDetail.ImageUrl

            };
            _context.CropDetails.Add(crop);
            return await _context.SaveChangesAsync() > 0;
        }

        // ✅ Update crop details
        public async Task<bool> UpdateCropDetail(int cropId, CropDetailVM cropDetail)
        {
            var existingCrop = await _context.CropDetails.FindAsync(cropId);
            if (existingCrop == null) return false;

            existingCrop.CropName = cropDetail.CropName;
            existingCrop.CropTypeId = cropDetail.CropTypeId;
            existingCrop.QuantityAvailable = cropDetail.QuantityAvailable;
            existingCrop.Price = cropDetail.Price;
            existingCrop.CropLocation = cropDetail.CropLocation;
            existingCrop.Status = cropDetail.Status;

            _context.CropDetails.Update(existingCrop);
            return await _context.SaveChangesAsync() > 0;
        }

        // ✅ Delete a crop
        public async Task<bool> DeleteCropDetail(int cropId)
        {
    var crop = await _context.CropDetails.FindAsync(cropId);
    if (crop == null) return false;

    _context.CropDetails.Remove(crop);
    return await _context.SaveChangesAsync() > 0;
        }

// ✅ Get crop by name
public async Task<CropDetail> GetCropDetailByName(string cropName)
{
    return await _context.CropDetails.FirstOrDefaultAsync(c => c.CropName == cropName);
}

// ✅ Get crops within a price range
public async Task<IEnumerable<CropDetail>> GetCropsByPriceRange(decimal minPrice, decimal maxPrice)
{
    return await _context.CropDetails
        .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
        .ToListAsync();
}

// ✅ Get crops by location
public async Task<IEnumerable<CropDetail>> GetCropsByLocation(string location)
{
            return await _context.CropDetails
        .Where(c => EF.Functions.Like(c.CropLocation, $"%{location}%"))
        .ToListAsync();
 }

// ✅ Get the most expensive crop
public async Task<CropDetail> GetMostExpensiveCrop()
{
return await _context.CropDetails
    .OrderByDescending(c => c.Price)
    .FirstOrDefaultAsync();
}

// ✅ Get available crops (quantity > 0)
public async Task<IEnumerable<CropDetail>> GetAvailableCrops()
{
return await _context.CropDetails
    .Where(c => c.Status == "Available")
    .ToListAsync();
}

// ✅ Get out-of-stock crops (quantity = 0)
public async Task<IEnumerable<CropDetail>> GetOutOfStockCrops()
{
return await _context.CropDetails
    .Where(c => c.QuantityAvailable == 0)
    .ToListAsync();
}

// ✅ Get crops that are low in stock (below threshold)
public async Task<IEnumerable<CropDetail>> GetLowStockCrops(int threshold)
{
return await _context.CropDetails
    .Where(c => c.QuantityAvailable <= threshold)
    .ToListAsync();
}

// ✅ Get available crops for a specific farmer
public async Task<IEnumerable<CropDetail>> GetAvailableCropsByFarmer(int farmerId)
{
return await _context.CropDetails
    .Where(c => c.FarmerId == farmerId && c.QuantityAvailable > 0 && c.Status == "Available")
    .ToListAsync();
}

// ✅ Get crops sold by a specific farmer
public async Task<IEnumerable<CropDetail>> GetCropsByFarmerId(int farmerId)
{
return await _context.CropDetails
    .Where(c => c.FarmerId == farmerId)
    .ToListAsync();
}

// ✅ Get crops filtered by farmer & crop type
public async Task<IEnumerable<CropDetail>> GetCropsByFarmerAndType(int farmerId, int cropTypeId)
{
return await _context.CropDetails
    .Where(c => c.FarmerId == farmerId && c.CropTypeId == cropTypeId)
    .ToListAsync();
}

// ✅ Get crops within a price range for a specific farmer
public async Task<IEnumerable<CropDetail>> GetCropsByFarmerAndPriceRange(int farmerId, decimal minPrice, decimal maxPrice)
{
return await _context.CropDetails
    .Where(c => c.FarmerId == farmerId && c.Price >= minPrice && c.Price <= maxPrice)
    .ToListAsync();
}

// ✅ Get crops sorted by name
public async Task<IEnumerable<CropDetail>> GetCropsSortedByName()
{
return await _context.CropDetails
    .OrderBy(c => c.CropName)
    .ToListAsync();
}

// ✅ Get crops with pagination
public async Task<IEnumerable<CropDetail>> GetCropsWithPagination(int pageNumber, int pageSize)
{
return await _context.CropDetails
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();
}

// ✅ Get the total crop count
public async Task<int> GetTotalCropCount()
{
return await _context.CropDetails.CountAsync();
}

// ✅ Get sold crops
public async Task<IEnumerable<CropDetail>> GetSoldCrops()
{
return await _context.CropDetails
    .Where(c => c.Status == "Sold")
    .ToListAsync();
}

// ✅ Check if a crop exists by ID
public async Task<bool> CropExists(int cropId)
{
return await _context.CropDetails.AnyAsync(c => c.Id == cropId);
}

// ✅ Count available crops for a specific farmer
public async Task<int> CountAvailableCropsByFarmer(int farmerId)
{
return await _context.CropDetails
    .CountAsync(c => c.FarmerId == farmerId && c.QuantityAvailable > 0);
}

// ✅ Count total crops of a specific type
public async Task<int> CountCropsByType(int cropTypeId)
{
return await _context.CropDetails
    .CountAsync(c => c.CropTypeId == cropTypeId);
}

// Get crops by type
public async Task<IEnumerable<CropDetail>> GetCropsByType(int cropTypeId)
{
return await _context.CropDetails
    .Where(c => c.CropTypeId == cropTypeId)
    .ToListAsync();
}

        // Get recently restocked crops (within 'days' number of days)
        public async Task<IEnumerable<CropDetail>> GetRecentlyRestockedCrops(int days)
        {
            DateTime recentDate = DateTime.UtcNow.AddDays(-days);

            return await _context.CropDetails
                .Include(c => c.Farmer)
                .Where(c => c.UpdatedAt >= recentDate) // Assuming you have a RestockDate field
                .OrderByDescending(c => c.UpdatedAt)
                .ToListAsync();
        }

        public async Task<bool> UpdateCropAvailability(int cropId, decimal newQuantity)
        {
            var crop = await _context.CropDetails.FindAsync(cropId);

            if (crop == null)
                return false;

            crop.QuantityAvailable = newQuantity;
            crop.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        // Get crops by status (e.g., "Available", "Sold", "Pending")
        public async Task<IEnumerable<CropDetail>> GetCropsByStatus(string status)
{
return await _context.CropDetails
    .Where(c => c.Status.ToLower() == status.ToLower())
    .ToListAsync();
}

// Get crops with seller details (assuming a relationship exists)
public async Task<IEnumerable<CropDetail>> GetCropsWithSeller()
{
            return await _context.CropDetails
                .Include(c => c.Farmer).OrderBy(c => c.FarmerId) 
                .ToListAsync();
        }

        // Get recently added crops (sorted by newest first)
        public async Task<IEnumerable<CropDetail>> GetRecentCrops()
        {
            return await _context.CropDetails
                .Include(c => c.Farmer)
                .OrderByDescending(c => c.CreatedAt) 
                .Take(10)
                .ToListAsync();
        }

        // Get crops grouped by category
        public async Task<IEnumerable<IEnumerable<CropDetail>>> GetCropsGroupedByCategory()
{
    return await _context.CropDetails
        .GroupBy(c => c.CropTypeId)
                .ToListAsync();
        }

        // Get popular crop types based on total sales (assuming a Sales table exists)
        public async Task<IEnumerable<CropDetail>> GetPopularCropTypes()
{
    return await _context.CropDetails
        .OrderByDescending(c => c.Invoices.Count) // Assuming Sales is a navigation property
        .Take(5) // Get top 5 most popular crops
        .ToListAsync();
}
    }
}
