using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}
