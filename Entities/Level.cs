namespace AcademyApp.Entities
{
    public class Level : Entity<int>
    {
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}