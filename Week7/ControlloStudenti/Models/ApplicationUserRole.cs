using Microsoft.AspNetCore.Identity;

namespace spiegazione_REST_epicode.Models {
    public class ApplicationUserRole : IdentityUserRole<string> {

        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
