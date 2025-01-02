namespace AcademyApp.Entities
{
    public sealed class Category : Entity<int>
    {
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
