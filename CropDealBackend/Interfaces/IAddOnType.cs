using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface IAddonType
    {
        Task<IEnumerable<AddOnType>> GetAllAddonTypes();
        Task<AddOnType> GetAddonTypeById(int id);
        Task<bool> AddAddonType(AddonTypeVM addonType);
        Task<bool> UpdateAddonType(int id,AddonTypeVM addonType);
        Task<bool> DeleteAddonType(int id);
        // Retrieves addon types that were recently added in the last X days.
        //Task<IEnumerable<AddOnType>> GetRecentlyAddedAddonTypes(int days);

        // Retrieves the most popular addon types based on purchase frequency.
        Task<IEnumerable<AddOnType>> GetPopularAddonTypes();
    }
}
