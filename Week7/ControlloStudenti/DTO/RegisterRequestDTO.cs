using System.ComponentModel.DataAnnotations;

namespace spiegazione_REST_epicode.DTO {
    public class RegisterRequestDTO {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
