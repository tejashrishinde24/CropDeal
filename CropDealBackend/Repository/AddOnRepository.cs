using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDealBackend.Repositories
{
    public class AddOnRepository : IAddOn
    {
        private CropDealContext _context;
        public AddOnRepository(CropDealContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AddOn>> GetAllAddons()
        {
            return await _context.AddOns.ToListAsync();
        }

        public async Task<AddOn> GetAddonById(int id)
        {
            var result = await _context.AddOns.FindAsync(id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<bool> AddAddon(AddOn addon)
        {
            if (addon == null)
            {
                return false;
            }
            await _context.AddOns.AddAsync(addon);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAddon(AddOn addon)
        {
            if (addon == null || addon.AddOnId <= 0)
            {
                return false;
            }

            var existingaddon = await _context.AddOns.FindAsync(addon.AddOnId);
            if (existingaddon == null)
            {
                return false;
            }
            existingaddon.AddOnId = addon.AddOnId;
            existingaddon.AdminId = addon.AdminId;
            existingaddon.Admin = addon.Admin;
            existingaddon.AddOnTypeId = addon.AddOnTypeId;
            existingaddon.AddOnType = addon.AddOnType;
            existingaddon.Description = addon.Description;
            existingaddon.PricePerUnit = addon.PricePerUnit;
            existingaddon.Quantity = addon.Quantity;

            _context.AddOns.Update(existingaddon); // Mark entity as modified

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAddon(int id)
        {
            var addon = await _context.AddOns.FindAsync(id);
            if (addon == null)
            {
                return false;
            }
            _context.AddOns.Remove(addon);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AddOn>> GetAddonsByAdminId(int adminId)
        {
            return await _context.AddOns.Where(x => x.AdminId == adminId).ToListAsync();
        }

        public async Task<IEnumerable<AddOn>> GetAddonsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var result = await _context.AddOns.Where(x => x.PricePerUnit >= minPrice && x.PricePerUnit <= maxPrice).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AddOn>> GetAddonByAddOnType(int id)
        {
            var res = await _context.AddOns.Where(x => x.AddOnTypeId == id).ToListAsync();
            return res;
        }

        public async Task<bool> UpdateAddonQuantity(int addonId, int newQuantity)
        {
            if (addonId == null)
            {
                return false;
            }
            var result = await _context.AddOns.FindAsync(addonId);
            if (result == null)
            {
                return false;
            }
            result.Quantity = newQuantity;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsAddonAvailable(int addonId)
        {
            var result = await _context.AddOns.FindAsync(addonId);
            if (result != null && result.Quantity > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<AddOn>> SearchAddons(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new List<AddOn>(); // Return an empty list if keyword is null or empty
            }

            return await _context.AddOns
                .Where(a => a.AddOnName.Contains(keyword) || _context.AddOnTypes
                            .Any(t => t.AddOnTypeId == a.AddOnTypeId &&
                                     t.AddOnTypeName.Contains(keyword)))
                .ToListAsync();
        }

        public async Task<bool> ReduceAddonStock(int addonId, int quantity)
        {
            var result = await _context.AddOns.FindAsync(addonId);

            if (result == null || result.Quantity < quantity) // Ensure sufficient stock
            {
                return false;
            }

            result.Quantity -= quantity;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IncreaseAddonStock(int addonId, int quantity)
        {
            if (quantity <= 0)
            {
                return false; // Prevent increasing stock by zero or negative numbers
            }

            var addonstock = await _context.AddOns.FindAsync(addonId);
            if (addonstock == null)
            {
                return false;
            }

            addonstock.Quantity += quantity;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAddonPrice(int addonId, decimal newPrice)
        {
            if (newPrice <= 0)
            {
                return false; // Prevent setting a non-positive price
            }

            if (addonId == null)
            {
                return false;
            }

            var result = await _context.AddOns.FindAsync(addonId);
            if (result == null)
            {
                return false;
            }

            result.PricePerUnit = newPrice;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AddOn>> GetRecentlyAddedAddons(int days)
        {
            if (days <= 0)
            {
                return new List<AddOn>(); // Return an empty list for invalid input
            }

            DateTime fromDate = DateTime.UtcNow.AddDays(-days);
            return await _context.AddOns.Where(a => a.CreatedAt >= fromDate).ToListAsync();
        }

        public async Task<IEnumerable<AddOn>> GetTopSellingAddons(int topN)
        {
            if (topN <= 0)
            {
                return new List<AddOn>(); // Return an empty list for invalid input
            }

            return await _context.AddOns.OrderByDescending(a => a.Quantity).Take(topN).ToListAsync();
        }

        public async Task<IEnumerable<AddOn>> GetLeastSellingAddons(int bottomN)
        {
            if (bottomN <= 0)
            {
                return new List<AddOn>(); // Return an empty list for invalid input
            }

            return await _context.AddOns.OrderBy(x => x.Quantity).Take(bottomN).ToListAsync();
        }
    }
}