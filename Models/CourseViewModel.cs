using AcademyApp.Entities;

namespace AcademyApp.Models
{
    public class CourseViewModel
    {
        public Course OneCourse { get; set; }
        public IEnumerable<Category> Categories { get; set; } = null!;
        public IEnumerable<Course> Courses { get; set; } = null!;
        public IEnumerable<Level> Levels { get; set; } = null!;
    }
}