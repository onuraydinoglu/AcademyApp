using AcademyApp.Models;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AcademyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICourseRepository _courseRepository;

        public HomeController(ICategoryRepository categoryRepository, ICourseRepository courseRepository)
        {
            _categoryRepository = categoryRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var courses = await _courseRepository.GetAllCoursesAsync();

            var homeViewModel = new HomeViewModel
            {
                Categories = categories,
                Courses = courses
            };
            return View(homeViewModel);
        }
    }
}
