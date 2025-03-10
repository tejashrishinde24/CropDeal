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
        Task<IEnumerable<AddOnType>> GetPopularAddonTypes();
    }
}
