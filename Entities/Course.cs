using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AcademyApp.Entities
{
    public sealed class Course : Entity<int, DateTime>
    {
        private string? _title;

        [Required(ErrorMessage = "Please enter a title.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be 5-100 characters.")]
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Url = GenerateUrl(value);
            }
        }

        [Required(ErrorMessage = "Please enter a description.")]
        [StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
        public string Description { get; set; }

        public string? Image { get; set; }
        public string? Url { get; private set; }
        public bool IsCompleted { get; set; }

        [Required(ErrorMessage = "Please enter the total hours.")]
        [Range(1, 1000, ErrorMessage = "Hours must be between 1 and 1000.")]
        public int Hours { get; set; }

        public int? Rating { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required(ErrorMessage = "Please select the course level.")]
        public int? LevelId { get; set; }
        public Level? Level { get; set; }

        [Required(ErrorMessage = "Please select a Instructor.")]
        public int? UserId { get; set; }
        public User? User { get; set; }

        private static string GenerateUrl(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return string.Empty;

            string url = title.ToLowerInvariant();
            url = Regex.Replace(url, "\\s+", "-");
            url = Regex.Replace(url, "[^a-z0-9-]", string.Empty);

            return url;
        }

    }
}
