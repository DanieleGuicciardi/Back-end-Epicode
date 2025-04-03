using System.ComponentModel.DataAnnotations;

namespace spiegazione_REST_epicode.DTO {
    public class StudentUpdateDTO {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 16)]
        public string FiscalCode { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public List<int> CourseIds { get; set; }
    }
}
