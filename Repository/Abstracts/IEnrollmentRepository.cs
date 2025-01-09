using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetAllEnrollmentsdAsync();
        Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(int userId);
    }
}