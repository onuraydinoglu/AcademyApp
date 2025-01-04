using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace AcademyApp.Controllers
{
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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User Users)
        {
            await _UserRepository.AddUserAsync(Users);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var User = await _UserRepository.GetByIdUserAsync(id);
            return View(User);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User Users)
        {
            await _UserRepository.UpdateUserAsync(Users);
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