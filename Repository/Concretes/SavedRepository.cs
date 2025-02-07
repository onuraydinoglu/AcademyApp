using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Repository.Concretes
{
    public class SavedRepository : BaseRepository<Saved>, ISavedRepository
    {
        private readonly AppDbContext _context;

        public SavedRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Saved>> GetAllSavedAsync(int userId)
        {
            var saveds = await _context.Saveds.Include(x => x.User).Include(x => x.Course).Where(x => x.UserId == userId).ToListAsync();
            return saveds;
        }

        public async Task<List<int>> GetUserAndCoursesAsync(int userId)
        {
            var savedIds = await _context.Saveds.Where(x => x.UserId == userId).Select(x => x.CourseId).ToListAsync();
            return savedIds;
        }

    }
}