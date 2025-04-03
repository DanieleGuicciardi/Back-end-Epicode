using System.ComponentModel.DataAnnotations;

namespace spiegazione_REST_epicode.DTO {
    public class StudentCreateDTO {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(16)]
        public string FiscalCode { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MinLength(1)]
        public List<int> CourseIds { get; set; }
    }
}
