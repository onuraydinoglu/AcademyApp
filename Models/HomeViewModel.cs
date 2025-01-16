using AcademyApp.Entities;

namespace AcademyApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = null!;
        public IEnumerable<Course> Courses { get; set; } = null!;
        public Course Course { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; } = null!;
    }
}