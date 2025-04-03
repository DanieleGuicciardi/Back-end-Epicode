using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace spiegazione_REST_epicode.Models {
    public class StudentProfile {

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FiscalCode { get; set; }
        public DateTime BirthDate { get; set; }

        // Navigazione inversa (opzionale)
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }

}
