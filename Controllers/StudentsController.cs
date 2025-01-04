using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademyApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentsController : Controller
    {
        private readonly IUserRepository _UserRepository;

        public StudentsController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Users = await _UserRepository.GetAllUsersAsync();
            return View(Users);
        }
    }
}