using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetAllUsersByRoleIdAsync(int id);
        Task<User> LoginAsync(string Email, string Password);
        Task<User> GetByIdUserAsync(int? id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
