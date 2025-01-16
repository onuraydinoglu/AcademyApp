using System.ComponentModel.DataAnnotations;

namespace AcademyApp.Entities
{
    public abstract class Entity<TId, TCreateDate>
    {
        [Key]
        public TId Id { get; set; }

        public TCreateDate CreatedDate { get; set; }

        protected Entity()
        {
            if (typeof(TCreateDate) == typeof(DateTime))
            {
                CreatedDate = (TCreateDate)(object)DateTime.Now;
            }
        }
    }
}
