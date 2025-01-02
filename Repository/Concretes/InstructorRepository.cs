using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Repository.Concretes
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AppDbContext _context;

        public InstructorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync()
        {
            var instructors = await _context.Instructors.ToListAsync();
            return instructors;
        }

        public Task<Instructor> GetByIdInstructorAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task AddInstructorAsync(Instructor instructor)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInstructorAsync(Instructor instructor)
        {
            throw new NotImplementedException();
        }

        public Task DeleteInstructorAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
