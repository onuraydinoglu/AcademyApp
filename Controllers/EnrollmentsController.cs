using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Security.Claims;
using System.Text;

namespace AcademyApp.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentsController(IEnrollmentRepository enrollmentRepository, IUserRepository userRepository, ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentRepository.GetAllEnrollmentsdAsync();
            return View(enrollments);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = new SelectList(await _userRepository.GetAllAsync(), "Id", "FullName");
            ViewBag.Courses = new SelectList(await _courseRepository.GetAllAsync(), "Id", "Title");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Enrollment enrollment)
        {
            // Seçilen UserId ve CourseId'yi kontrol ediyoruz.
            var isEnrolled = await _enrollmentRepository.IsEnrolledAsync(enrollment.UserId, enrollment.CourseId);
            if (isEnrolled)
            {
                TempData["ErrorMessage"] = "The selected user is already enrolled in this course.";
                return RedirectToAction("Create");
            }

            // Kayıt işlemini gerçekleştiriyoruz.
            await _enrollmentRepository.AddAsync(enrollment);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            ViewBag.Users = new SelectList(await _userRepository.GetAllAsync(), "Id", "FullName");
            ViewBag.Courses = new SelectList(await _courseRepository.GetAllAsync(), "Id", "Title");
            return View(enrollment);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Enrollment enrollment)
        {
            await _enrollmentRepository.UpdateAsync(enrollment);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _enrollmentRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MyCourses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var enrollments = await _enrollmentRepository.GetEnrollmentsByUserIdAsync(int.Parse(userId));
            return View(enrollments);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserCreate(int courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Eğer Kayıtlıysa
            var isEnrolled = await _enrollmentRepository.IsEnrolledAsync(int.Parse(userId), courseId);
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (isEnrolled)
            {
                TempData["ErrorMessage"] = "You are already enrolled in this course.";
                return RedirectToAction("Detail", "Courses", new { id = courseId, url = course.Url });
            }

            var enrollment = new Enrollment
            {
                CourseId = courseId,
                UserId = int.Parse(userId)
            };

            await _enrollmentRepository.AddAsync(enrollment);
            var enrolledCourse = await _courseRepository.GetByIdAsync(courseId);
            return RedirectToAction("Detail", "Courses", new { id = courseId, url = enrolledCourse.Url });
        }
    }
}