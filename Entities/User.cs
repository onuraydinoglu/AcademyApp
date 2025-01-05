using System.ComponentModel.DataAnnotations;

namespace AcademyApp.Entities
{
    public sealed class User : Entity<int>
    {
        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 100 characters.")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 100 characters.")]

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Please enter a password with at least 6 characters.")]
        public string Password { get; set; }

        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
