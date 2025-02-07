using AcademyApp.Entities;
using AcademyApp.Models;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AcademyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ISavedRepository _savedRepository;

        public HomeController(ICategoryRepository categoryRepository, ICourseRepository courseRepository, IEnrollmentRepository enrollmentRepository, ISavedRepository savedRepository)
        {
            _categoryRepository = categoryRepository;
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _savedRepository = savedRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var savedIds = new List<int>();
            int? userId = userIdClaim != null ? int.Parse(userIdClaim) : (int?)null;

            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var courses = await _courseRepository.GetAllCoursesAsync();

            var enrollments = userId != null
                ? await _enrollmentRepository.GetEnrollmentsByUserIdAsync(userId.Value)
                : Enumerable.Empty<Enrollment>();

            if (userIdClaim is not null)
            {
                savedIds = await _savedRepository.GetUserAndCoursesAsync(int.Parse(userIdClaim));
            }

            ViewBag.SavedIds = savedIds;

            var homeViewModel = new HomeViewModel
            {
                Categories = categories,
                Courses = courses,
                Enrollments = enrollments,
            };

            return View(homeViewModel);
        }
    }
}
