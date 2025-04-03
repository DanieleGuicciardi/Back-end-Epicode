using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spiegazione_REST_epicode.Data;
using spiegazione_REST_epicode.DTO;
using spiegazione_REST_epicode.Models;

namespace spiegazione_REST_epicode.Controllers {

    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase {

        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() {
            try {
                List<Student> students = await _context.Students
                    .Include(s => s.Courses)
                    .Include(s => s.StudentProfile)
                    .ToListAsync();


                List<StudentDTO> studentList = students.Select(s => new StudentDTO {
                    Id = s.Id,
                    FirstName = s.StudentProfile.FirstName,
                    LastName = s.StudentProfile.LastName,
                    Courses = s.Courses.Select(c => new CourseDTO {
                        Title = c.Title,
                        Description = c.Description
                    }).ToList()
                }).ToList();

                return Ok(studentList);
            }
            catch (Exception) {
                return BadRequest(); 
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetById(int id) {

            try {
                Student student = await _context.Students
                    .Include(s => s.StudentProfile)
                    .Include(s => s.Courses)
                    .FirstOrDefaultAsync(s => s.Id == id);


                if (student == null) {
                    return BadRequest();
                }
                StudentDTO studentDTO = new StudentDTO {
                    Id = student.Id,
                    FirstName = student.StudentProfile.FirstName,
                    LastName = student.StudentProfile.LastName,
                    Courses = student.Courses.Select(c => new CourseDTO {
                        Title = c.Title,
                        Description = c.Description
                    }).ToList()
                };

                return Ok(studentDTO); 
            }
            catch (Exception) {
                return BadRequest(); 
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDTO StudentCreateDto) {
            // Validazione del modello
            if (!ModelState.IsValid) {
                return BadRequest(); 
            }

            try {
                // Crea la parte anagrafica
                var profile = new StudentProfile {
                    FirstName = StudentCreateDto.FirstName,
                    LastName = StudentCreateDto.LastName,
                    FiscalCode = StudentCreateDto.FiscalCode,
                    BirthDate = StudentCreateDto.BirthDate
                };

                // Recupera i corsi dal database in base agli ID forniti
                var courses = await _context.Courses
                    .Where(c => StudentCreateDto.CourseIds.Contains(c.Id))
                    .ToListAsync();

                // Crea lo studente con profilo e corsi associati
                var student = new Student {
                    StudentProfile = profile,
                    Courses = courses
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                // Restituisce 201 Created con l’URL della risorsa appena creata
                return Created();
            }
            catch (Exception ex) {
               
                return BadRequest(new { error = "Errore durante la creazione dello studente"});
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                // Recupera lo studente con include per evitare problemi con relazioni
                var student = await _context.Students
                    //.Include(s => s.StudentProfile)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (student == null) {
                    return BadRequest();
                }

                // Rimuove lo studente (il profilo sarà rimosso in cascata se configurato)
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex) {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDTO StudentUpdateDTO) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            try {
                var student = await _context.Students
                    .Include(s => s.StudentProfile)
                    .Include(s => s.Courses)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (student == null) {
                    return BadRequest();
                }

                // Aggiorna il profilo
                student.StudentProfile.FirstName = StudentUpdateDTO.FirstName;
                student.StudentProfile.LastName = StudentUpdateDTO.LastName;
                student.StudentProfile.FiscalCode = StudentUpdateDTO.FiscalCode;
                student.StudentProfile.BirthDate = StudentUpdateDTO.BirthDate;

                // Aggiorna i corsi
                var newCourses = await _context.Courses
                    .Where(c => StudentUpdateDTO.CourseIds.Contains(c.Id))
                    .ToListAsync();

                student.Courses = newCourses;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex) {
                return BadRequest();
            }
        }



    }
}


