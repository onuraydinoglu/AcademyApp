using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace AcademyApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student students)
        {
            await _studentRepository.AddStudentAsync(students);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepository.GetByIdStudentAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student students)
        {
            await _studentRepository.UpdateStudentAsync(students);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentRepository.DeleteStudentAsync(id);
            return RedirectToAction("Index");
        }
    }
}
