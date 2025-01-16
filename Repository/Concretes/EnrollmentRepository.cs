using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Repository.Concretes
{
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsdAsync()
        {
            var enrollments = await _context.Enrollments.Include(x => x.Course).ThenInclude(x => x.Category).Include(x => x.User).ToListAsync();
            return enrollments;
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(int userId)
        {
            var enrollment = await _context.Enrollments.Include(x => x.Course).ThenInclude(x => x.Level).Where(e => e.UserId == userId).ToListAsync();
            return enrollment;
        }

        public Task<bool> IsEnrolledAsync(int? userId, int courseId)
        {
            var Enrollments = _context.Enrollments.AnyAsync(e => e.UserId == userId && e.CourseId == courseId);
            return Enrollments;
        }

        public async Task UpdateEnrollmentAsync(Enrollment enrollment)
        {
            var UpdateEnrollment = await GetByIdAsync(enrollment.Id);
            UpdateEnrollment.UserId = enrollment.UserId;
            UpdateEnrollment.CourseId = enrollment.CourseId;
            _context.Enrollments.Update(UpdateEnrollment);
            await _context.SaveChangesAsync();
        }
    }
}