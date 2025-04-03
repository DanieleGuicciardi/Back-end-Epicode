using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace spiegazione_REST_epicode.Models {
    public class Student {

            [Key]
            public int Id { get; set; }

            // Foreign key per StudentProfile
            //public int StudentProfileId { get; set; }

            // Navigazione verso il profilo dello studente
            public StudentProfile StudentProfile { get; set; }

            // Navigazione molti-a-molti verso i corsi
            public ICollection<Course> Courses { get; set; }
     }
}

