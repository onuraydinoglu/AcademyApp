using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Repository.Concretes
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses.Include(x => x.Category).Include(x => x.Level).Include(x => x.User).ThenInclude(x => x.Role).ToListAsync();
            return courses;
        }

        public async Task UpdateCourseAsync(Course course)
        {
            var crs = await GetByIdAsync(course.Id);
            crs.Title = course.Title;
            crs.Description = course.Description;
            crs.Image = course.Image;
            crs.LevelId = course.LevelId;
            crs.Hours = course.Hours;
            crs.Rating = course.Rating;
            crs.CategoryId = course.CategoryId;
            crs.UserId = course.UserId;
            _context.Courses.Update(crs);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await GetByIdAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
