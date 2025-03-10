using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CropDealBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropDetailController : ControllerBase
    {
        private readonly ICropDetail _cropDetailRepository;

        public CropDetailController(ICropDetail cropDetailRepository)
        {
            _cropDetailRepository = cropDetailRepository;
        }

        // ✅ Get all crops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetAllCrops()
        {
            var crops = await _cropDetailRepository.GetAllCrops();
            return Ok(crops);
        }

        // ✅ Get crop by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CropDetail>> GetCropById(int id)
        {
            var crop = await _cropDetailRepository.GetCropById(id);
            if (crop == null) return NotFound();
            return Ok(crop);
        }

        // ✅ Create a new crop
        [HttpPost]
        public async Task<ActionResult> CreateCrop([FromBody] CropDetailVM cropDetail)
        {
            var created = await _cropDetailRepository.CreateCropDetail(cropDetail);
            if (!created) return BadRequest("Failed to create crop.");
            return CreatedAtAction(nameof(GetCropById), new { id = cropDetail.Id }, cropDetail);
        }

        // ✅ Update crop details
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCrop(int id, [FromBody] CropDetailVM cropDetail)
        {
            var updated = await _cropDetailRepository.UpdateCropDetail(id, cropDetail);
            if (!updated) return NotFound("Crop not found or update failed.");
            return NoContent();
        }

        // ✅ Delete a crop
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCrop(int id)
        {
            var deleted = await _cropDetailRepository.DeleteCropDetail(id);
            if (!deleted) return NotFound("Crop not found.");
            return NoContent();
        }

        // ✅ Get crops by category
        [HttpGet("category/{cropTypeId}")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetCropsByCategory(int cropTypeId)
        {
            var crops = await _cropDetailRepository.GetCropsByCategory(cropTypeId);
            return Ok(crops);
        }

        // ✅ Get crops by location
        [HttpGet("location/{location}")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetCropsByLocation(string location)
        {
            var crops = await _cropDetailRepository.GetCropsByLocation(location);
            return Ok(crops);
        }

        // ✅ Get crops by price range
        [HttpGet("price-range")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetCropsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var crops = await _cropDetailRepository.GetCropsByPriceRange(minPrice, maxPrice);
            return Ok(crops);
        }

        // ✅ Get available crops
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetAvailableCrops()
        {
            var crops = await _cropDetailRepository.GetAvailableCrops();
            return Ok(crops);
        }

        // ✅ Get out-of-stock crops
        [HttpGet("out-of-stock")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetOutOfStockCrops()
        {
            var crops = await _cropDetailRepository.GetOutOfStockCrops();
            return Ok(crops);
        }

        // ✅ Get crops sorted by name
        [HttpGet("sorted/name")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetCropsSortedByName()
        {
            var crops = await _cropDetailRepository.GetCropsSortedByName();
            return Ok(crops);
        }

        // ✅ Get total crop count
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalCropCount()
        {
            var count = await _cropDetailRepository.GetTotalCropCount();
            return Ok(count);
        }




        // ✅ Get recently restocked crops
        [HttpGet("recently-restocked/{days}")]
        public async Task<IActionResult> GetRecentlyRestockedCrops(int days)
        {
            var crops = await _cropDetailRepository.GetRecentlyRestockedCrops(days);
            return Ok(crops);
        }

        // ✅ Get most expensive crop
        [HttpGet("most-expensive")]
        public async Task<IActionResult> GetMostExpensiveCrop()
        {
            var crop = await _cropDetailRepository.GetMostExpensiveCrop();
            return Ok(crop);
        }

        // ✅ Get crops by status (e.g., Available, Sold, Pending)
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetCropsByStatus(string status)
        {
            var crops = await _cropDetailRepository.GetCropsByStatus(status);
            return Ok(crops);
        }

        // ✅ Get crop by name
        [HttpGet("name/{cropName}")]
        public async Task<ActionResult<CropDetail>> GetCropDetailByName(string cropName)
        {
            var crop = await _cropDetailRepository.GetCropDetailByName(cropName);
            if (crop == null) return NotFound("Crop not found.");
            return Ok(crop);
        }

        // ✅ Get low stock crops (below threshold)
        [HttpGet("low-stock/{threshold}")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetLowStockCrops(int threshold)
        {
            var crops = await _cropDetailRepository.GetLowStockCrops(threshold);
            return Ok(crops);
        }

        // ✅ Get available crops for a specific farmer
        [HttpGet("available/farmer/{farmerId}")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetAvailableCropsByFarmer(int farmerId)
        {
            var crops = await _cropDetailRepository.GetAvailableCropsByFarmer(farmerId);
            return Ok(crops);
        }

        // ✅ Get crops by farmer ID
        [HttpGet("farmer/{farmerId}")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetCropsByFarmerId(int farmerId)
        {
            var crops = await _cropDetailRepository.GetCropsByFarmerId(farmerId);
            return Ok(crops);
        }

        // ✅ Get crops by farmer and crop type
        [HttpGet("farmer/{farmerId}/type/{cropTypeId}")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetCropsByFarmerAndType(int farmerId, int cropTypeId)
        {
            var crops = await _cropDetailRepository.GetCropsByFarmerAndType(farmerId, cropTypeId);
            return Ok(crops);
        }

        // ✅ Get crops by farmer and price range
        [HttpGet("farmer/{farmerId}/price-range")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetCropsByFarmerAndPriceRange(int farmerId, [FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var crops = await _cropDetailRepository.GetCropsByFarmerAndPriceRange(farmerId, minPrice, maxPrice);
            return Ok(crops);
        }

        // ✅ Get crops with pagination
        [HttpGet("paginate")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetCropsWithPagination([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var crops = await _cropDetailRepository.GetCropsWithPagination(pageNumber, pageSize);
            return Ok(crops);
        }

        // ✅ Get sold crops
        [HttpGet("sold")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetSoldCrops()
        {
            var crops = await _cropDetailRepository.GetSoldCrops();
            return Ok(crops);
        }

        // ✅ Check if a crop exists by ID
        [HttpGet("exists/{cropId}")]
        public async Task<ActionResult<bool>> CropExists(int cropId)
        {
            var exists = await _cropDetailRepository.CropExists(cropId);
            return Ok(exists);
        }

        // ✅ Count available crops for a specific farmer
        [HttpGet("count/available/farmer/{farmerId}")]
        public async Task<ActionResult<int>> CountAvailableCropsByFarmer(int farmerId)
        {
            var count = await _cropDetailRepository.CountAvailableCropsByFarmer(farmerId);
            return Ok(count);
        }

        // ✅ Count total crops of a specific type
        [HttpGet("count/type/{cropTypeId}")]
        public async Task<ActionResult<int>> CountCropsByType(int cropTypeId)
        {
            var count = await _cropDetailRepository.CountCropsByType(cropTypeId);
            return Ok(count);
        }

        // ✅ Get crops by type
        [HttpGet("type/{cropTypeId}")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetCropsByType(int cropTypeId)
        {
            var crops = await _cropDetailRepository.GetCropsByType(cropTypeId);
            return Ok(crops);
        }

        // ✅ Get crops with seller details
        [HttpGet("with-seller")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetCropsWithSeller()
        {
            var crops = await _cropDetailRepository.GetCropsWithSeller();
            return Ok(crops);
        }

        // ✅ Get recently added crops
        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetRecentCrops()
        {
            var crops = await _cropDetailRepository.GetRecentCrops();
            return Ok(crops);
        }

        // ✅ Get crops grouped by category
        [HttpGet("grouped/category")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetCropsGroupedByCategory()
        {
            var crops = await _cropDetailRepository.GetCropsGroupedByCategory();
            return Ok(crops);
        }

        // ✅ Get popular crop types
        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetPopularCropTypes()
        {
            var crops = await _cropDetailRepository.GetPopularCropTypes();
            return Ok(crops);
        }
    }
}
