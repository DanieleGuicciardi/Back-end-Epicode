using AjaxMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AjaxMvc.Data {

    // la classe ApplicationDbContext eredita da DbContext che è la classe "base" di EFC utilizzata per la gestione del database
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {

        // costruttore che accetta un ogetto di tipo DbContextOptions<ApplicationDbContext>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /* rappresenta la tabella Student all'interno del database. Grazie a EFC questa proprietà permette di eseguire query sulla
         * tabella Student come se fosse una collezione di ogetti in memoria 
         */
        public DbSet<Student> Students { get; set; }

    }
}




