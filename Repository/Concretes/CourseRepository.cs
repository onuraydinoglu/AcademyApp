using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Repository.Concretes
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses.Include(x => x.Category).ToListAsync();
            return courses;
        }

        public async Task<Course> GetByIdCourseAsync(int? id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course is null)
            {
                throw new Exception($"no course found by the id: {id} you are looking for");
            }
            return course;
        }

        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            var crs = await GetByIdCourseAsync(course.Id);
            crs.Title = course.Title;
            crs.Description = course.Description;
            crs.CategoryId = course.CategoryId;
            crs.InstructorId = course.InstructorId;
            _context.Courses.Update(crs);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await GetByIdCourseAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
