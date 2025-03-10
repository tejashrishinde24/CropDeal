using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using CropDealBackend.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CropDealBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        // GET: api/<UserDetailController>
        private readonly IUserDetail userDetailRepository;
        public UserDetailController(IUserDetail _userDetailRepository)
        {
            userDetailRepository = _userDetailRepository;
        }

        // ✅ Get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetAllUsers()
        {
            var users = await userDetailRepository.GetAllUsers();
            return Ok(users);
        }

        // ✅ Get user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetail>> GetUserById(int id)
        {
            var user = await userDetailRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            return Ok(user);
        }

        // ✅ Get user by Email
        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDetail>> GetUserByEmail(string email)
        {
            var user = await userDetailRepository.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            return Ok(user);
        }

        // ✅ Create user
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserDetail user)
        {
            if (user == null)
            {
                return BadRequest(new { Message = "Invalid user data" });
            }

            var result = await userDetailRepository.CreateUserDetail(user);
            if (result)
            {
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            return BadRequest(new { Message = "User creation failed" });
        }

        // ✅ Update user
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserDetail user)
        {
            if (user == null)
            {
                return BadRequest(new { Message = "Invalid user data" });
            }

            var result = await userDetailRepository.UpdateUserDetail(id, user);
            if (result)
            {
                return Ok(new { Message = "User updated successfully" });
            }
            return NotFound(new { Message = "User not found or update failed" });
        }

        // ✅ Delete user
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var result = await userDetailRepository.DeleteUserDetail(id);
            if (result)
            {
                return Ok(new { Message = "User deleted successfully" });
            }
            return NotFound(new { Message = "User not found or deletion failed" });
        }

        // ✅ Get users with pagination
        [HttpGet("paginated")]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetUsersWithPagination([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var users = await userDetailRepository.GetUsersWithPagination(pageNumber, pageSize);
            return Ok(users);
        }

        // ✅ Get users by role
        [HttpGet("role/{role}")]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetUsersByRole(string role)
        {
            var users = await userDetailRepository.GetUsersByRole(role);
            return Ok(users);
        }

        // ✅ Search users by name
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<UserDetail>>> SearchUsersByName(string name)
        {
            var users = await userDetailRepository.SearchUsersByName(name);
            return Ok(users);
        }

        // ✅ Get active users within the last N days
        [HttpGet("active/{days}")]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetActiveUsers(int days)
        {
            var users = await userDetailRepository.GetActiveUsers(days);
            return Ok(users);
        }

        // ✅ Get total users count
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalUsersCount()
        {
            var count = await userDetailRepository.GetTotalUsersCount();
            return Ok(count);
        }

        // ✅ Get inactive users for last N days
        [HttpGet("inactive/{days}")]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetInactiveUsers(int days)
        {
            var users = await userDetailRepository.GetInactiveUsers(days);
            return Ok(users);
        }

        // ✅ Get recently registered users
        [HttpGet("recently-registered/{days}")]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetRecentlyRegisteredUsers(int days)
        {
            var users = await userDetailRepository.GetRecentlyRegisteredUsers(days);
            return Ok(users);
        }

        // ✅ Get dealer IDs by Subscription ID
        [HttpGet("dealers-by-subscription/{subscriptionId}")]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetDealerIdBySubscription(int subscriptionId)
        {
            var users = await userDetailRepository.GetDealerIdBySubscription(subscriptionId);
            return Ok(users);
        }
    }
}
