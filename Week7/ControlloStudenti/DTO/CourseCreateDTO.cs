using System.ComponentModel.DataAnnotations;

namespace spiegazione_REST_epicode.DTO {
    public class CourseCreateDTO {

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }
    }
}
