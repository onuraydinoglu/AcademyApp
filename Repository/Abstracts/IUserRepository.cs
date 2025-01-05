using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetAllUsersByRoleIdAsync(int id);
        Task<User> LoginAsync(string Email, string Password);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
