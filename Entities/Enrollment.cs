namespace AcademyApp.Entities
{
    public sealed class Enrollment : Entity<int, DateTime>
    {
        public bool IsCompleted { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
