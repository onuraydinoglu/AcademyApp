using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp.Repository.Concretes
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var courses = await _context.Users.Include(x => x.Role).ToListAsync();
            return courses;
        }

        public async Task<IEnumerable<User>> GetAllUsersByRoleIdAsync(int id)
        {
            var usersRoleId = await _context.Users.Where(x => x.RoleId == id).ToListAsync();
            return usersRoleId;
        }

        public async Task<User> LoginAsync(string Email, string Password)
        {
            var isUser = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == Email && x.Password == Password);
            return isUser;
        }

        public async Task AddUserAsync(User user)
        {
            if (user.RoleId is null)
            {
                user.RoleId = 3;
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var usr = await GetByIdAsync(user.Id);
            usr.FirstName = user.FirstName;
            usr.LastName = user.LastName;
            usr.RoleId = user.RoleId;
            usr.Email = user.Email;
            usr.Password = user.Password;
            _context.Users.Update(usr);
            await _context.SaveChangesAsync();
        }
    }
}
