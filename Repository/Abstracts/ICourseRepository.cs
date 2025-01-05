using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
    }
}
