namespace AcademyApp.Entities
{
    public sealed class Enrollment : Entity<int>
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId {  get; set; }
        public Course Course { get; set;}
    }
}
