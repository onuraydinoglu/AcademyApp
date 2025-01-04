using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace AcademyApp.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User User)
        {
            await _UserRepository.AddUserAsync(User);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var User = await _UserRepository.GetByIdUserAsync(id);
            return View(User);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User User)
        {
            await _UserRepository.UpdateUserAsync(User);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _UserRepository.DeleteUserAsync(id);
            return RedirectToAction("Index");
        }
    }
}