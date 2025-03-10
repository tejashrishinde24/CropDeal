using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDealBackend.Repository
{
    public class UserDetailRepository:IUserDetail
    {
        private CropDealContext _context;
        public UserDetailRepository(CropDealContext context)
        {
            _context = context;
        }
        // ✅ Get all users
        public async Task<IEnumerable<UserDetail>> GetAllUsers()
        {
            return await _context.UserDetails.ToListAsync();
        }

        // ✅ Get user by ID
        public async Task<UserDetail> GetUserById(int userId)
        {
            return await _context.UserDetails.FindAsync(userId);
        }

        // ✅ Get user by Email
        public async Task<UserDetail> GetUserByEmail(string email)
        {
            return await _context.UserDetails.FirstOrDefaultAsync(u => u.EmailId == email);
        }

        // ✅ Create user
        public async Task<bool> CreateUserDetail(UserDetail user)
        {
            await _context.UserDetails.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        // ✅ Update user
        public async Task<bool> UpdateUserDetail(int userId, UserDetail user)
        {
            var existingUser = await _context.UserDetails.FindAsync(userId);
            if (existingUser == null) return false;

            existingUser.Name = user.Name;
            existingUser.Role = user.Role;
            existingUser.Address = user.Address;
            existingUser.EmailId = user.EmailId;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.LoginId = user.LoginId;
            existingUser.Password = user.Password;
            existingUser.IsActive = user.IsActive;
            //existingUser.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        // ✅ Delete user
        public async Task<bool> DeleteUserDetail(int userId)
        {
            var user = await _context.UserDetails.FindAsync(userId);
            if (user == null) return false;

            _context.UserDetails.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        // ✅ Get users with pagination
        public async Task<IEnumerable<UserDetail>> GetUsersWithPagination(int pageNumber, int pageSize)
        {
            return await _context.UserDetails
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // ✅ Get users by role
        public async Task<IEnumerable<UserDetail>> GetUsersByRole(string role)
        {
            return await _context.UserDetails
                .Where(u => u.Role == role)
                .ToListAsync();
        }

        // ✅ Search users by name (partial matching)
        public async Task<IEnumerable<UserDetail>> SearchUsersByName(string name)
        {
            return await _context.UserDetails
                .Where(u => u.Name.Contains(name))
                .ToListAsync();
        }

        // ✅ Get active users within last N days
        public async Task<IEnumerable<UserDetail>> GetActiveUsers(int days)
        {
            DateTime cutoffDate = DateTime.UtcNow.AddDays(-days);
            return await _context.UserDetails
                .Where(u => u.IsActive == true && u.Id > 0)
                .ToListAsync();
        }

        // ✅ Get total count of users
        public async Task<int> GetTotalUsersCount()
        {
            return await _context.UserDetails.CountAsync();
        }

        // ✅ Get inactive users for last N days
        public async Task<IEnumerable<UserDetail>> GetInactiveUsers(int days)
        {
            DateTime cutoffDate = DateTime.UtcNow.AddDays(-days);
            return await _context.UserDetails
                .Where(u => u.IsActive == false)
                .ToListAsync();
        }

        // ✅ Get users who registered in the last N days
        public async Task<IEnumerable<UserDetail>> GetRecentlyRegisteredUsers(int days)
        {
            DateTime cutoffDate = DateTime.UtcNow.AddDays(-days);
            return await _context.UserDetails
                .Where(u => u.Id > 0)
                .ToListAsync();
        }

        // ✅ Get Dealer IDs based on Subscription ID
        public async Task<IEnumerable<UserDetail>> GetDealerIdBySubscription(int subscribeId)
        {
            return await _context.UserDetails
                .Where(u => u.Subscriptions.Any(s => s.Id == subscribeId))
                .ToListAsync();
        }
    }
}
