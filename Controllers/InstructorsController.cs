using AcademyApp.Entities;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademyApp.Controllers
{
  public class InstructorsController : Controller
  {
    private readonly IInstructorRepository _instructorRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ICategoryRepository _categoryRepository;

    public InstructorsController(IInstructorRepository instructorRepository, ICourseRepository courseRepository, ICategoryRepository categoryRepository)
    {
      _instructorRepository = instructorRepository;
      _courseRepository = courseRepository;
      _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var instructors = await _instructorRepository.GetAllInstructorsAsync();
      return View(instructors);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
      ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
      ViewBag.Courses = new SelectList(await _courseRepository.GetAllCoursesAsync(), "Id", "Title");
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Instructor instructor)
    {
      await _instructorRepository.AddInstructorAsync(instructor);
      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      var instructor = await _instructorRepository.GetByIdInstructorAsync(id);
      return View(instructor);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Instructor instructor)
    {
      await _instructorRepository.UpdateInstructorAsync(instructor);
      return RedirectToAction("Index");
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
      await _instructorRepository.DeleteInstructorAsync(id);
      return RedirectToAction("Index");
    }
  }
}
