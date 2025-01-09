using AcademyApp.Entities;

namespace AcademyApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = null!;
        public IEnumerable<Course> Courses { get; set; } = null!;
        public IEnumerable<Enrollment> Enrollments { get; set; } = null!;
    }
}