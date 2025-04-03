using System.ComponentModel.DataAnnotations;

namespace spiegazione_REST_epicode.Controllers {
    public class TokenResponse {

        [Required]
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
