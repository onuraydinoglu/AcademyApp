using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course> GetByUrlAsync(string? url);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
    }
}
