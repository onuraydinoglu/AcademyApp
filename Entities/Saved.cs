namespace AcademyApp.Entities
{
    public class Saved : Entity<int, DateTime>
    {
        public int? UserId { get; set; }
        public User User { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}