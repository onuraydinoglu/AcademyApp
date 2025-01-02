namespace AcademyApp.Entities
{
    public sealed class Course : Entity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public int Hours { get; set; }
        public int? Rating { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
    }
}
