using Microsoft.AspNetCore.Identity;

namespace spiegazione_REST_epicode.Models {
    public class ApplicationRole : IdentityRole {

        public ICollection<ApplicationUserRole> ApplicationUsers { get; set; }
    }
}
