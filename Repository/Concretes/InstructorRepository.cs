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

        public async Task<Instructor> GetByIdInstructorAsync(int? id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor is null)
            {
                throw new Exception($"no instructor found by the id: {id} you are looking for");
            }
            return instructor;
        }

        public async Task AddInstructorAsync(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInstructorAsync(Instructor instructor)
        {
            var ins = await GetByIdInstructorAsync(instructor.Id);
            ins.FirstName = instructor.FirstName;
            ins.LastName = instructor.LastName;
            ins.Email = instructor.Email;
            _context.Instructors.Update(ins);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInstructorAsync(int id)
        {
            var instructor = await GetByIdInstructorAsync(id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
        }
    }
}
