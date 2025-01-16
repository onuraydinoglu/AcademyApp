using System.ComponentModel.DataAnnotations;

namespace AcademyApp.Entities
{
    public sealed class Category : Entity<int, DateTime>
    {
        [Required(ErrorMessage = "Name field is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? Url { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
