using Microsoft.AspNetCore.Identity;

namespace AjaxMvc.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }
    }
}
