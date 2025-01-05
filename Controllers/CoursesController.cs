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

        public CoursesController(ICourseRepository courseRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, ILevelRepository levelRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _levelRepository = levelRepository;
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
        public async Task<IActionResult> Detail(int id)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var courses = await _courseRepository.GetAllCoursesAsync();
            var course = await _courseRepository.GetByIdAsync(id);
            var level = await _levelRepository.GetAllAsync();
            var courseViewModel = new CourseViewModel
            {
                Categories = categories,
                Courses = courses,
                Course = course,
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
