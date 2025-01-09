using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AcademyApp.Controllers
{
    [Authorize]
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentsController(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var enrollment = new Enrollment
            {
                CourseId = courseId,
                UserId = int.Parse(userId)
            };

            await _enrollmentRepository.AddAsync(enrollment);
            return RedirectToAction("Detail", "Courses", new
            {
                id = courseId
            });
        }
    }
}