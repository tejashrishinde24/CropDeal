using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDealBackend.Repository
{
    public class AddOnTypeRepository:IAddonType
    {
        private CropDealContext _context;
        public AddOnTypeRepository(CropDealContext context)
        {
            _context = context;
        }
        // ✅ Get all Add-on Types
        public async Task<IEnumerable<AddOnType>> GetAllAddonTypes()
        {
            return await _context.AddOnTypes.OrderBy(c=>c.AddOnTypeId).ToListAsync();
        }

        // ✅ Get Add-on Type by ID
        public async Task<AddOnType> GetAddonTypeById(int id)
        {
            return await _context.AddOnTypes.FindAsync(id);
        }

        // ✅ Add new Add-on Type
        public async Task<bool> AddAddonType(AddonTypeVM type)
        {
            if (type == null) return false;
            AddOnType addonType = new AddOnType() {
                AddOnTypeName = type.AddOnTypeName
                
            };
            await _context.AddOnTypes.AddAsync(addonType);
            return await _context.SaveChangesAsync() > 0;
        }

        // ✅ Update Add-on Type
        public async Task<bool> UpdateAddonType(int id,AddonTypeVM addonType)
        {
            var existingAddOnType = await _context.AddOnTypes.FindAsync(id);
            if (existingAddOnType == null) return false;

            existingAddOnType.AddOnTypeName = addonType.AddOnTypeName;
            _context.AddOnTypes.Update(existingAddOnType);
            return await _context.SaveChangesAsync() > 0;
        }

        // ✅ Delete Add-on Type
        public async Task<bool> DeleteAddonType(int id)
        {
            var addonType = await _context.AddOnTypes.FindAsync(id);
            if (addonType == null) return false;

            _context.AddOnTypes.Remove(addonType);
            return await _context.SaveChangesAsync() > 0;
        }

        // ✅ Get Add-on Types recently added in last X days
        //public async Task<IEnumerable<AddOnType>> GetRecentlyAddedAddonTypes(int days)
        //{
        //    DateTime recentDate = DateTime.UtcNow.AddDays(-days);
        //    return await _context.AddOnTypes
        //        .Where(a => EF.Property<DateTime>(a, "CreatedDate") >= recentDate)
        //        .ToListAsync();
        //}

        // ✅ Get most popular Add-on Types (based on purchase frequency)
        public async Task<IEnumerable<AddOnType>> GetPopularAddonTypes()
        {
            return await _context.AddOnTypes
                .OrderByDescending(a => a.AddOns.Count)
                .Take(5)// Assuming AddOns table tracks purchases
                .ToListAsync();
        }
    }
}
