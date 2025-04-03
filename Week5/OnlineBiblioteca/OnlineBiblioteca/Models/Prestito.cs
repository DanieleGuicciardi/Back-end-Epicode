using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    public class Prestito
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public required string NomeUtente { get; set; }

        [Required]
        [EmailAddress]
        public required string EmailUtente { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime DataPrestito { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? DataRestituzione { get; set; } = DateTime.Now.AddDays(14);

        [DataType(DataType.DateTime)]
        public DateTime? DataRestituzioneEffettiva { get; set; }

        public string? Note { get; set; }

        // Relazione con il libro - definita qui nel modello
        public Guid LibroId { get; set; }

        [ForeignKey("LibroId")]
        public virtual required Libro? Libro { get; set; }

        [NotMapped]
        public bool Scaduto { get; set; }
    }
}
