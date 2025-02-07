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
        private readonly ISavedRepository _savedRepository;

        public UsersController(IUserRepository userRepository, IRoleRepository roleRepository, ISavedRepository savedRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _savedRepository = savedRepository;
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
                        new Claim(ClaimTypes.Name, login.FullName),
                        new Claim(ClaimTypes.Email, login.Email)
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
            if (ModelState.IsValid)
            {
                await _userRepository.AddUserAsync(user);
                return RedirectToAction("Login", "Users");
            }
            return View(user);
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            // Kullanıcı kimliğini al
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Veritabanından kullanıcıyı al
            var user = await _userRepository.GetByIdAsync(int.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var user = await _userRepository.GetByIdAsync(int.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Roles = new SelectList(await _roleRepository.GetAllAsync(), "Id", "Name");
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(User user)
        {
            await _userRepository.UpdateUserAsync(user);
            return RedirectToAction("Profile", "Users");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Saved()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Unauthorized();
            }

            var saveds = await _savedRepository.GetAllSavedAsync(int.Parse(userId));
            return View(saveds);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SavedCreate(int courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Unauthorized();
            }

            var saved = new Saved
            {
                UserId = int.Parse(userId),
                CourseId = courseId
            };

            await _savedRepository.AddAsync(saved);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SavedDelete(int courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Unauthorized();
            }

            var savedIds = await _savedRepository.GetAllSavedAsync(int.Parse(userId));
            var deleteCourse = savedIds.FirstOrDefault(x => x.CourseId == courseId);

            if (deleteCourse is not null)
            {
                await _savedRepository.DeleteAsync(deleteCourse.Id);
            }

            return RedirectToAction("Index", "Home");
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
            ViewBag.Roles = new SelectList(await _roleRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.AddUserAsync(user);
                return RedirectToAction("Index");
            }
            ViewBag.Roles = new SelectList(await _roleRepository.GetAllAsync(), "Id", "Name");
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Roles = new SelectList(await _roleRepository.GetAllAsync(), "Id", "Name");
            var User = await _userRepository.GetByIdAsync(id);
            return View(User);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.UpdateUserAsync(user);
                return RedirectToAction("Index");
            }
            ViewBag.Roles = new SelectList(await _roleRepository.GetAllAsync(), "Id", "Name");
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
