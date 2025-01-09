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

        public HomeController(ICategoryRepository categoryRepository, ICourseRepository courseRepository, IEnrollmentRepository enrollmentRepository)
        {
            _categoryRepository = categoryRepository;
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            // Giriş yapmış kullanıcının ID'sini al
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? userId = userIdClaim != null ? int.Parse(userIdClaim) : (int?)null;

            // Kategorileri ve kursları al
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var courses = await _courseRepository.GetAllCoursesAsync();

            // Kullanıcıya özel kayıtları al
            var enrollments = userId != null
                ? await _enrollmentRepository.GetEnrollmentsByUserIdAsync(userId.Value)
                : Enumerable.Empty<Enrollment>();

            // ViewModel oluştur
            var homeViewModel = new HomeViewModel
            {
                Categories = categories,
                Courses = courses,
                Enrollments = enrollments
            };

            return View(homeViewModel);
        }
    }
}
