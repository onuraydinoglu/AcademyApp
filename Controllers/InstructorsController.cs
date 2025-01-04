using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademyApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstructorsController : Controller
    {
        private readonly IUserRepository _UserRepository;

        public InstructorsController(IUserRepository UserRepository)
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