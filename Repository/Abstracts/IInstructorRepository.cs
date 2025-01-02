using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();
        Task<Instructor> GetByIdInstructorAsync(int? id);
        Task AddInstructorAsync(Instructor instructor);
        Task UpdateInstructorAsync(Instructor instructor);
        Task DeleteInstructorAsync(int id);
    }
}
