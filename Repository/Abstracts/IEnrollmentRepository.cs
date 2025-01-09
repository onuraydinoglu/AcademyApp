using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync(int userId);
    }
}