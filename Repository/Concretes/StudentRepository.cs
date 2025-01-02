using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Repository.Concretes
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var courses = await _context.Students.ToListAsync();
            return courses;
        }

        public async Task<Student> GetByIdStudentAsync(int? id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student is null)
            {
                throw new Exception($"no student found by the id: {id} you are looking for");
            }
            return student;
        }

        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var std = await GetByIdStudentAsync(student.Id);
            std.FirstName = student.FirstName;
            std.LastName = student.LastName;
            std.Email = student.Email;
            _context.Students.Update(std);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await GetByIdStudentAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
