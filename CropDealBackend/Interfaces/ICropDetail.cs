using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface ICropDetail
    {
        Task<IEnumerable<CropDetail>> GetAllCrops();
        Task<CropDetail> GetCropById(int cropId);
        Task<bool> CreateCropDetail(CropDetailVM cropDetail);
        Task<bool> UpdateCropDetail(int cropId, CropDetailVM cropDetail);
        Task<bool> DeleteCropDetail(int cropId);

        // Crop Lookup Methods
        Task<CropDetail> GetCropDetailByName(string cropName);
        Task<IEnumerable<CropDetail>> GetCropsByCategory(int cropTypeId);
        Task<IEnumerable<CropDetail>> GetCropsByType(int cropTypeId);

        // Filtering & Sorting
        Task<IEnumerable<CropDetail>> GetCropsByPriceRange(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<CropDetail>> GetCropsByLocation(string location);
        Task<IEnumerable<CropDetail>> GetCropsSortedByName();
        Task<IEnumerable<CropDetail>> GetCropsWithPagination(int pageNumber, int pageSize);

        // Farmer-Specific Queries
        Task<IEnumerable<CropDetail>> GetCropsByFarmerId(int farmerId);
        Task<IEnumerable<CropDetail>> GetCropsByFarmerAndPriceRange(int farmerId, decimal minPrice, decimal maxPrice);
        Task<IEnumerable<CropDetail>> GetAvailableCropsByFarmer(int farmerId);
        Task<IEnumerable<CropDetail>> GetCropsByFarmerAndType(int farmerId, int cropTypeId);
        Task<int> CountAvailableCropsByFarmer(int farmerId);

        // Availability & Status
        Task<IEnumerable<CropDetail>> GetAvailableCrops();  // Quantity > 0
        Task<IEnumerable<CropDetail>> GetOutOfStockCrops();  // Quantity = 0
        Task<IEnumerable<CropDetail>> GetLowStockCrops(int threshold);
        Task<IEnumerable<CropDetail>> GetRecentlyRestockedCrops(int days);
        Task<IEnumerable<CropDetail>> GetCropsByStatus(string status);
        Task<IEnumerable<CropDetail>> GetSoldCrops();  // Status = "Sold"

        // Advanced Queries
        Task<CropDetail> GetMostExpensiveCrop();
        Task<IEnumerable<CropDetail>> GetCropsWithSeller();
        Task<int> GetTotalCropCount();
        Task<bool> UpdateCropAvailability(int cropId, decimal newQuantity);
        Task<IEnumerable<CropDetail>> GetRecentCrops();
        Task<IEnumerable<IEnumerable<CropDetail>>> GetCropsGroupedByCategory();
        Task<IEnumerable<CropDetail>> GetPopularCropTypes(); // Most sold or in demand
        Task<int> CountCropsByType(int cropTypeId);

        // Utility
        Task<bool> CropExists(int cropId);
    }
}
