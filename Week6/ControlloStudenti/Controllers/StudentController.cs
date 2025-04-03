using AjaxMvc.Data;
using AjaxMvc.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AjaxMvc.Controllers {
    public class StudentController : Controller {
        private readonly ILogger<StudentController> _logger;
        private readonly ApplicationDbContext _context;

        public StudentController(ILogger<StudentController> logger, ApplicationDbContext context) {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index() {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> List() {
            try {
                // recover list of all users.
               List<Student> students = await _context.Students.ToListAsync();
                return PartialView("~/Views/Shared/UC_StudentList.cshtml", students);
            }
            catch (Exception ex) {
                this._logger.LogError(ex.Message);
            }
            return PartialView("~/Views/Shared/UC_StudentList.cshtml", new List<Student>());

        }
        [HttpGet]
        public async Task<ActionResult> Edit(int Id) {
            Student student = new Student();            
            try {
                student = await _context.Students.FirstOrDefaultAsync(s => s.Id == Id);
                 
            }
            catch (Exception ex) {
                this._logger.LogError(ex.Message);
            }
            return PartialView("~/Views/Shared/UC_StudentForm.cshtml", student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Student student) {
            if (!ModelState.IsValid) {
                return Json(new { success = false, message = "Dati non validi" });
            }

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Studente salvato con successo" });
        }


        public async Task<ActionResult> Delete(int Id) {
            try {
                if (Id == 0) {
                    return Json(new { Success = false, Message = "Error deleting student." });
                }

                Student student = await _context.Students.FindAsync(Id);
                if (student == null) {
                    return Json(new { Success = false, Message = "Error deleting student." });
                }


                 _context.Students.Remove(student);
                bool result = await _context.SaveChangesAsync() > 0;



                if (result) {
                    return Json(new { Success = true, Message = "Operation completed successfully." });
                } else {
                    return Json(new { Success = false, Message = "Error" });
                }
            }
            catch (Exception ex) {
                this._logger.LogError(ex.Message);
            }
            return Json(new { Success = false, Message = "Error deleting student." });
        }

    }
}
