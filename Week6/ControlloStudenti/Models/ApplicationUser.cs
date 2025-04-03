using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AjaxMvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required DateOnly BirthDate { get; set; }

        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }
    }
}
