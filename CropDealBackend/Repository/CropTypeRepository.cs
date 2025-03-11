using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDealBackend.Repository
{
    public class CropTypeRepository:ICropType
    {
        private  CropDealContext _context;
        public CropTypeRepository(CropDealContext context)
        {
            _context = context;
        }
        // ✅ Get all Crop Types
        public async Task<IEnumerable<CropType>> GetAllCropTypes()
        {
            return await _context.CropTypes.OrderBy(c=>c.CropTypeId).ToListAsync();
        }

        // ✅ Get Crop Type by ID
        public async Task<CropType> GetCropTypeById(int cropTypeId)
        {
            return await _context.CropTypes.FindAsync(cropTypeId);
        }

        // ✅ Add a new Crop Type
        public async Task<bool> CreateCropType(CropType cropType)
        {
            if (cropType == null) return false;

            await _context.CropTypes.AddAsync(cropType);
            return await _context.SaveChangesAsync() > 0;
        }

        // ✅ Update an existing Crop Type
        public async Task<bool> UpdateCropType(int cropTypeId, CropType cropType)
        {
            var existingCropType = await _context.CropTypes.FindAsync(cropTypeId);
            if (existingCropType == null) return false;

            existingCropType.CropTypeName = cropType.CropTypeName;

            _context.CropTypes.Update(existingCropType);
            return await _context.SaveChangesAsync() > 0;
        }

        // ✅ Delete a Crop Type by ID
        public async Task<bool> DeleteCropType(int cropTypeId)
        {
            var cropType = await _context.CropTypes.FindAsync(cropTypeId);
            if (cropType == null) return false;

            _context.CropTypes.Remove(cropType);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<CropType>> GetPopularCropTypes()
        {
            return await _context.CropTypes
                .OrderByDescending(ct => ct.CropDetails.Count)
                .ToListAsync();
        }
    }
}
