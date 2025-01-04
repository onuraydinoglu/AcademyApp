using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademyApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UsersController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return View(users);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            await _userRepository.AddUserAsync(user);
            return RedirectToAction("Login", "Users");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = new SelectList(await _roleRepository.GetAllRolesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await _userRepository.AddUserAsync(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Roles = new SelectList(await _roleRepository.GetAllRolesAsync(), "Id", "Name");
            var User = await _userRepository.GetByIdUserAsync(id);
            return View(User);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            await _userRepository.UpdateUserAsync(user);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return RedirectToAction("Index");
        }
    }
}