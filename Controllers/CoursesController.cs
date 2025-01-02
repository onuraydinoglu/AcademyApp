using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademyApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IInstructorRepository _instructorRepository;

        public CoursesController(ICourseRepository courseRepository, ICategoryRepository categoryRepository, IInstructorRepository instructorRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _instructorRepository = instructorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
            ViewBag.Instructors = new SelectList(await _instructorRepository.GetAllInstructorsAsync(), "Id", "FullName");
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
