using AcademyApp.Entities;
using AcademyApp.Models;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademyApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly ILevelRepository _levelRepository;

        public CoursesController(ICourseRepository courseRepository, ICategoryRepository categoryRepository, IInstructorRepository instructorRepository, ILevelRepository levelRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _instructorRepository = instructorRepository;
            _levelRepository = levelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var courses = await _courseRepository.GetAllCoursesAsync();
            var level = await _levelRepository.GetAllLevelsAsync();

            var courseViewModel = new CourseViewModel
            {
                Categories = categories,
                Courses = courses,
                Levels = level
            };
            return View(courseViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
            ViewBag.Instructors = new SelectList(await _instructorRepository.GetAllInstructorsAsync(), "Id", "FullName");
            ViewBag.Levels = new SelectList(await _levelRepository.GetAllLevelsAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            await _courseRepository.AddCourseAsync(course);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
            ViewBag.Instructors = new SelectList(await _instructorRepository.GetAllInstructorsAsync(), "Id", "FullName");
            ViewBag.Levels = new SelectList(await _levelRepository.GetAllLevelsAsync(), "Id", "Name");
            var course = await _courseRepository.GetByIdCourseAsync(id);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            await _courseRepository.UpdateCourseAsync(course);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseRepository.DeleteCourseAsync(id);
            return RedirectToAction("Index");
        }
    }
}
