namespace AcademyApp.Entities
{
    public sealed class Instructor : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
