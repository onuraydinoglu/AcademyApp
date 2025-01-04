namespace AcademyApp.Entities
{
    public class Role : Entity<int>
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}