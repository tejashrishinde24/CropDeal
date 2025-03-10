using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface IAdmin
    {
        IEnumerable<Admin> GetAdmins();

        Task<bool> CreateAdmin(Admin admin);

        Task<bool> UpdateAdmin(int adminId,Admin admin);


    }
}
