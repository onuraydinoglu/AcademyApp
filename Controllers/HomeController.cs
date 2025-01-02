using AcademyApp.Models;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AcademyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public HomeController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return View(courses);
        }
    }
}
