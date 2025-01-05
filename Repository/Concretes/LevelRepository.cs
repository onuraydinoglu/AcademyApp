using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Repository.Concretes
{
    public class LevelRepository : BaseRepository<Level>, ILevelRepository
    {
        private readonly AppDbContext _context;
        public LevelRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
