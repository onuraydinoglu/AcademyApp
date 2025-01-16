using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetAllEnrollmentsdAsync();
        Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(int userId);
        Task UpdateEnrollmentAsync(Enrollment enrollment);
        Task<bool> IsEnrolledAsync(int? userId, int courseId);
    }
}