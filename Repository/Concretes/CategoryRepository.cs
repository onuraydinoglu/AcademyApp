using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Repository.Concretes
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var ctg = await GetByIdAsync(category.Id);
            ctg.Name = category.Name;
            ctg.Image = category.Image;
            _context.Categories.Update(ctg);
            await _context.SaveChangesAsync();
        }
    }
}
