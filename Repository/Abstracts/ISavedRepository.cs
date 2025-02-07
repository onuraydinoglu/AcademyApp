using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface ISavedRepository : IBaseRepository<Saved>
    {
        Task<IEnumerable<Saved>> GetAllSavedAsync(int userId);
        Task<List<int>> GetUserAndCoursesAsync(int userId);
    }
}