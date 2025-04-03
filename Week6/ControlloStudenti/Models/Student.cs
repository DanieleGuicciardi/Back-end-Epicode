using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AjaxMvc.Models {
    public class Student {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display]
        public DateTime DataDiNascita { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
