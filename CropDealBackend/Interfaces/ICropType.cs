﻿using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface ICropType
    {
        Task<IEnumerable<CropType>> GetAllCropTypes();
        Task<CropType> GetCropTypeById(int cropTypeId);
        Task<bool> CreateCropType(CropType cropType);
        Task<bool> UpdateCropType(int cropTypeId, CropType cropType);
        Task<bool> DeleteCropType(int cropTypeId);
        Task<IEnumerable<CropType>> GetPopularCropTypes();
    }
}
