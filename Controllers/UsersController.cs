using System.Security.Claims;
using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string password)
        {
            if (ModelState.IsValid)
            {
                // Doğru kullanıcı bilgilerini kontrol et
                var login = await _userRepository.LoginAsync(Email, password);

                if (login != null)
                {
                    var userClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, login.Id.ToString()),
                        new Claim(ClaimTypes.Name, login.FullName ?? ""),
                        new Claim(ClaimTypes.Email, login.Email ?? "")
                    };

                    if (login.Role != null)
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, login.Role.Name));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true // Kalıcı oturum
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Kullanıcı adı veya şifre yanlışsa hata mesajı ekle
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // Giriş başarısızsa formu tekrar göster
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            await _userRepository.AddUserAsync(user);
            return RedirectToAction("Login", "Users");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = new SelectList(await _roleRepository.GetAllRolesAsync(), "Id", "Name");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await _userRepository.AddUserAsync(user);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Roles = new SelectList(await _roleRepository.GetAllRolesAsync(), "Id", "Name");
            var User = await _userRepository.GetByIdUserAsync(id);
            return View(User);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            await _userRepository.UpdateUserAsync(user);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return RedirectToAction("Index");
        }
    }
}
