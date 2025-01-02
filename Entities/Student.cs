namespace AcademyApp.Entities
{
    public sealed class Student : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { 
            get
                {
                return FirstName + " " + LastName;
            } 
        }
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
