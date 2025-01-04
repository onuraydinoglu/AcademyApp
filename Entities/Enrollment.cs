namespace AcademyApp.Entities
{
    public sealed class Enrollment : Entity<int>
    {
        public int? UserId { get; set; }
        public User User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
