using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Data;
using StudentPortal.Models.Entities;
using StudentPortal.Models;
using Microsoft.EntityFrameworkCore;


namespace StudentPortal.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext dbContext;

        public StudentController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();



            return RedirectToAction("List", "Student");
        }


        [HttpGet]

        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);

        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid Id)
        {
            var student = await dbContext.Students.FindAsync(Id);
            return View(student);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.Id);

            if(student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Student");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.Id);
            if (student is not null)
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Student");

        }
    }
}
