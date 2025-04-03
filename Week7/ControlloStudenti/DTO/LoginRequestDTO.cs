using System.ComponentModel.DataAnnotations;

namespace spiegazione_REST_epicode.DTO {
    public class LoginRequestDTO {

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
