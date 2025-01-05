using System.ComponentModel.DataAnnotations;

namespace AcademyApp.Entities
{
    public abstract class Entity<TId>
    {
        [Key]
        public TId Id { get; set; }
    }
}
