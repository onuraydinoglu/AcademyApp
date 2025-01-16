using System.Security.Claims;
using AcademyApp.Entities;
using AcademyApp.Models;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademyApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public CoursesController(ICourseRepository courseRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, ILevelRepository levelRepository, IEnrollmentRepository enrollmentRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _levelRepository = levelRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var courses = await _courseRepository.GetAllCoursesAsync();
            var level = await _levelRepository.GetAllAsync();

            var courseViewModel = new CourseViewModel
            {
                Categories = categories,
                Courses = courses,
                Levels = level
            };
            return View(courseViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id, string url)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var courses = await _courseRepository.GetAllCoursesAsync();
            var coursee = await _courseRepository.GetByUrlAsync(url);
            var level = await _levelRepository.GetAllAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isEnrolled = false;

            // Eğer kullanıcı giriş yaptıysa, kursa kayıtlı olup olmadığını kontrol ediyoruz.
            if (userId != null)
            {
                isEnrolled = await _enrollmentRepository.IsEnrolledAsync(int.Parse(userId), id);
            }

            // ViewBag ile Razor View'e kayıt durumunu taşıyoruz.
            ViewBag.IsEnrolled = isEnrolled;

            var courseViewModel = new CourseViewModel
            {
                Categories = categories,
                Courses = courses,
                Course = coursee,
                Levels = level
            };
            return View(courseViewModel);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return View(courses);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
            ViewBag.Users = new SelectList(await _userRepository.GetAllUsersByRoleIdAsync(2), "Id", "FullName");
            ViewBag.Levels = new SelectList(await _levelRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseRepository.AddAsync(course);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
            ViewBag.Users = new SelectList(await _userRepository.GetAllUsersByRoleIdAsync(2), "Id", "FullName");
            ViewBag.Levels = new SelectList(await _levelRepository.GetAllAsync(), "Id", "Name");
            return View(course);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
            ViewBag.Users = new SelectList(await _userRepository.GetAllUsersByRoleIdAsync(2), "Id", "FullName");
            ViewBag.Levels = new SelectList(await _levelRepository.GetAllAsync(), "Id", "Name");
            var course = await _courseRepository.GetByIdAsync(id);
            return View(course);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseRepository.UpdateCourseAsync(course);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
            ViewBag.Users = new SelectList(await _userRepository.GetAllUsersByRoleIdAsync(2), "Id", "FullName");
            ViewBag.Levels = new SelectList(await _levelRepository.GetAllAsync(), "Id", "Name");
            return View(course);
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseRepository.DeleteCourseAsync(id);
            return RedirectToAction("Index");
        }
    }
}
