using CropDealBackend.Models;

namespace CropDealBackend.Interfaces
{
    public interface IUserDetail
    {
        Task<IEnumerable<UserDetail>> GetAllUsers();
        Task<UserDetail> GetUserById(int userId);
        Task<UserDetail> GetUserByEmail(string email);
        Task<bool> CreateUserDetail(UserDetailVM user);
        Task<bool> UpdateUserDetail(int userId,UserDetailVM user);
        Task<bool> DeleteUserDetail(int userId);
        // Retrieves a paginated list of users.
        Task<IEnumerable<UserDetail>> GetUsersWithPagination(int pageNumber, int pageSize);

        // Retrieves users by their role.
        Task<IEnumerable<UserDetail>> GetUsersByRole(string role);

        // Searches users by name using partial matching.
        Task<IEnumerable<UserDetail>> SearchUsersByName(string name);

        // Retrieves users active within the last specified days.
        Task<IEnumerable<UserDetail>> GetActiveUsers(int days);

        // Returns the total count of users.
        Task<int> GetTotalUsersCount();

        // Retrieves users inactive for the specified number of days.
        Task<IEnumerable<UserDetail>> GetInactiveUsers(int days);

        // Retrieves users who registered in the last specified days.
        Task<IEnumerable<UserDetail>> GetRecentlyRegisteredUsers(int days);
        Task<IEnumerable<UserDetail>> GetDealerIdBySubscription(int subscribeId);
        Task<bool> UpdateUserPassword(int userId, string newPassword);
    }
}
