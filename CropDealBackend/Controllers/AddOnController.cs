using Microsoft.AspNetCore.Mvc;
using CropDealBackend.Interfaces;
using CropDealBackend.Repositories;
using CropDealBackend.Models;
using Microsoft.EntityFrameworkCore;


namespace CropDealBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddOnController : ControllerBase
    {
        private readonly IAddOn addonrepo;
        public AddOnController(IAddOn _addonrepo)
        {
            addonrepo = _addonrepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddOn>>> Get()
        {
            var addons = await addonrepo.GetAllAddons();

            if (addons.Any())
            {
                return Ok(addons);
            }
            return BadRequest();
        }

        // GET api/<AddOnController>/5
        [HttpGet("{id}")]

        public async Task<ActionResult<AddOn>> GetAddonById(int id)
        {
            var result = await addonrepo.GetAddonById(id);

            if (result == null)
            {
                return NotFound(new { message = $"AddOn with ID {id} not found." });
            }

            return Ok(result);
        }
        // POST api/<AddOnController>


        [HttpPost]
        public async Task<ActionResult> AddAddon([FromBody] AddOn addon)
        {
            if (addon == null)
            {
                return BadRequest(new { message = "Invalid AddOn data." });
            }

            bool isAdded = await addonrepo.AddAddon(addon);

            if (!isAdded)
            {
                return BadRequest(new { message = "Failed to add AddOn." });
            }

            return StatusCode(201);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddon(int id, [FromBody] AddOn addon)
        {
            if (addon == null || id != addon.AddOnId)
            {
                return BadRequest(); // 400 Bad Request
            }

            bool isUpdated = await addonrepo.UpdateAddon(addon);

            if (!isUpdated)
            {
                return NotFound(); // 404 Not Found
            }

            return NoContent(); // 204 No Content (success without a response body)
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddon(int id)
        {
            bool isDeleted = await addonrepo.DeleteAddon(id);

            if (!isDeleted)
            {
                return NotFound(); // 404 Not Found
            }

            return NoContent(); // 204 No Content (successful deletion)
        }


        [HttpGet("admin/{adminId}")]
        public async Task<IActionResult> GetAddonsByAdminId(int adminId)
        {
            var addons = await addonrepo.GetAddonsByAdminId(adminId);

            if (addons == null || !addons.Any())
            {
                return NotFound(); // 404 Not Found if no add-ons exist for the admin
            }

            return Ok(addons); // 200 OK with the list of add-ons
        }


        [HttpGet("price-range")]
        public async Task<IActionResult> GetAddonsByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
            {
                return BadRequest(); // 400 Bad Request for invalid input
            }

            var addons = await addonrepo.GetAddonsByPriceRange(minPrice, maxPrice);

            if (addons == null || !addons.Any())
            {
                return NotFound(); // 404 Not Found if no add-ons exist in the range
            }

            return Ok(addons); // 200 OK with the list of add-ons
        }

        [HttpGet("type/{id}")]
        public async Task<IActionResult> GetAddonByAddOnType(int id)
        {
            var addons = await addonrepo.GetAddonByAddOnType(id);

            if (addons == null || !addons.Any())
            {
                return NotFound(); // 404 Not Found if no add-ons exist for the given type
            }

            return Ok(addons); // 200 OK with the list of add-ons
        }


        [HttpPatch("{addonId}/quantity/{newQuantity}")]
        public async Task<IActionResult> UpdateAddonQuantity(int addonId, int newQuantity)
        {
            bool isUpdated = await addonrepo.UpdateAddonQuantity(addonId, newQuantity);

            if (!isUpdated)
            {
                return NotFound(); // 404 Not Found if add-on doesn't exist
            }

            return NoContent(); // 204 No Content (successful update)
        }

        [HttpGet("{addonId}/availability")]
        public async Task<IActionResult> IsAddonAvailable(int addonId)
        {
            bool isAvailable = await addonrepo.IsAddonAvailable(addonId);

            return Ok(isAvailable); // 200 OK with true/false
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchAddons([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(); // 400 Bad Request for invalid keyword
            }

            var addons = await addonrepo.SearchAddons(keyword);

            if (addons == null || !addons.Any())
            {
                return NotFound(); // 404 Not Found if no matching add-ons are found
            }

            return Ok(addons); // 200 OK with the matching add-ons
        }


        [HttpPatch("{addonId}/reduce-stock/{quantity}")]
        public async Task<IActionResult> ReduceAddonStock(int addonId, int quantity)
        {
            if (quantity <= 0)
            {
                return BadRequest(); // 400 Bad Request for invalid quantity
            }

            bool isReduced = await addonrepo.ReduceAddonStock(addonId, quantity);

            if (!isReduced)
            {
                return NotFound(); // 404 Not Found or insufficient stock
            }

            return NoContent(); // 204 No Content for a successful update
        }


        [HttpPatch("{addonId}/increase-stock/{quantity}")]
        public async Task<IActionResult> IncreaseAddonStock(int addonId, int quantity)
        {
            if (quantity <= 0)
            {
                return BadRequest(); // 400 Bad Request for invalid quantity
            }

            bool isIncreased = await addonrepo.IncreaseAddonStock(addonId, quantity);

            if (!isIncreased)
            {
                return NotFound(); // 404 Not Found if add-on doesn't exist
            }

            return NoContent(); // 204 No Content for a successful update
        }

        [HttpPatch("{addonId}/update-price/{newPrice}")]
        public async Task<IActionResult> UpdateAddonPrice(int addonId, decimal newPrice)
        {
            if (newPrice <= 0)
            {
                return BadRequest(); // 400 Bad Request for invalid price
            }

            bool isUpdated = await addonrepo.UpdateAddonPrice(addonId, newPrice);

            if (!isUpdated)
            {
                return NotFound(); // 404 Not Found if the add-on doesn't exist
            }

            return NoContent(); // 204 No Content for a successful update
        }

        [HttpGet("recent/{days}")]
        public async Task<IActionResult> GetRecentlyAddedAddons(int days)
        {
            if (days <= 0)
            {
                return BadRequest(); // 400 Bad Request for invalid days value
            }

            var addons = await addonrepo.GetRecentlyAddedAddons(days);

            if (addons == null || !addons.Any())
            {
                return NotFound(); // 404 Not Found if no add-ons exist
            }

            return Ok(addons); // 200 OK with the recently added add-ons
        }


        [HttpGet("top-selling/{topN}")]
        public async Task<IActionResult> GetTopSellingAddons(int topN)
        {
            if (topN <= 0)
            {
                return BadRequest(); // 400 Bad Request for invalid topN value
            }

            var addons = await addonrepo.GetTopSellingAddons(topN);

            if (addons == null || !addons.Any())
            {
                return NotFound(); // 404 Not Found if no add-ons exist
            }

            return Ok(addons); // 200 OK with the top-selling add-ons
        }


        [HttpGet("least-selling/{bottomN}")]
        public async Task<IActionResult> GetLeastSellingAddons(int bottomN)
        {
            if (bottomN <= 0)
            {
                return BadRequest(); // 400 Bad Request for invalid bottomN value
            }

            var addons = await addonrepo.GetLeastSellingAddons(bottomN);

            if (addons == null || !addons.Any())
            {
                return NotFound(); // 404 Not Found if no add-ons exist
            }

            return Ok(addons); // 200 OK with the least-selling add-ons
        }


    }
}