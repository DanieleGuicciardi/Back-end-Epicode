using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spiegazione_REST_epicode.Data;
using spiegazione_REST_epicode.DTO;
using spiegazione_REST_epicode.Models;

namespace spiegazione_REST_epicode.Controllers {
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase {


        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCreateDTO CourseCreateDto) {

            try {

                if (!ModelState.IsValid) {
                    return BadRequest();
                }

                Course course = new Course {
                    Title = CourseCreateDto.Title,
                    Description = CourseCreateDto.Description
                };

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                return Created();
            }

            catch (Exception) {

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {

            try {
                Course course = await _context.Courses.FindAsync(id);
                if (course == null) {
                    return NotFound();
                }

                return Ok(new CourseDTO {
                    Title = course.Title,
                    Description = course.Description
                });
            }
            catch (Exception) {

                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                // Recupera tutti i corsi dal database, includendo gli studenti associati
                List<Course> courses = await _context.Courses
                    .Include(c => c.Students)
                    .ToListAsync();

                // Mappa i corsi alle loro rappresentazioni DTO
                var courseAllDTOs = courses.Select(c => new CourseAllDTO {
                    Title = c.Title,
                    Description = c.Description,
                    StudentCount = c.Students.Count // Numero di studenti iscritti al corso
                }).ToList();

                return Ok(courseAllDTOs);
            }
            catch (Exception ex) {
                // Gestione degli errori
                return BadRequest();
            }
        }
    }

}
