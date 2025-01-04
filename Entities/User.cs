namespace AcademyApp.Entities
{
    public sealed class User : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
