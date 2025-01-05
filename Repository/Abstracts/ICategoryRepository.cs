using AcademyApp.Entities;

namespace AcademyApp.Repository.Abstracts
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task UpdateCategoryAsync(Category category);
    }
}
