using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface IAddOn
    {
        Task<IEnumerable<AddOn>> GetAllAddons();
        Task<AddOn> GetAddonById(int id);
        Task<IEnumerable<AddOn>> GetAddonByAddOnType(int id);
        Task<bool> AddAddon(AddOn addon);
        Task<bool> UpdateAddon(AddOn addon);
        Task<bool> DeleteAddon(int id);


        Task<IEnumerable<AddOn>> GetAddonsByAdminId(int adminId); // Get addons added by a specific admin  
        Task<IEnumerable<AddOn>> GetAddonsByPriceRange(decimal minPrice, decimal maxPrice); // Get addons within a price range  
        Task<bool> UpdateAddonQuantity(int addonId, int newQuantity); // Update stock quantity  
        Task<bool> IsAddonAvailable(int addonId); // Check if the addon is in stock  

        Task<IEnumerable<AddOn>> SearchAddons(string keyword); // Search addons by name or description  
        Task<bool> ReduceAddonStock(int addonId, int quantity); // Reduce stock after purchase  
        Task<bool> IncreaseAddonStock(int addonId, int quantity); // Increase stock when new addons arrive  
        Task<bool> UpdateAddonPrice(int addonId, decimal newPrice); // Modify the price of an addon  
        Task<IEnumerable<AddOn>> GetRecentlyAddedAddons(int days); // Get addons added in the last 'n' days  
        Task<IEnumerable<AddOn>> GetTopSellingAddons(int topN); // Get the most purchased addons  
        Task<IEnumerable<AddOn>> GetLeastSellingAddons(int bottomN); // Get the least purchased addons  
    }

}