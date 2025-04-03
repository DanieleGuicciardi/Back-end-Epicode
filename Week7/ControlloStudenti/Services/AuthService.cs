using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using spiegazione_REST_epicode.Settings;

namespace spiegazione_REST_epicode.Services {
    public class AuthService {

        private readonly Identity _identitySettings;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private ApplicationDbContext _context;


        public AuthService(
            IOptions<Identity> identitySettings
            //ApplicationDbContext context,
            //UserManager<ApplicationUser> userManager

            ) {
            this._identitySettings = identitySettings.Value;
            //this._userManager = userManager;
            //this._context = context;

        }


        public string GenerateRandomPassword() {
            PasswordOptions opts = new PasswordOptions() {
                RequiredLength = _identitySettings.RequiredLength,
                RequireDigit = _identitySettings.RequireDigit,
                RequireLowercase = _identitySettings.RequireLowercase,
                RequireNonAlphanumeric = _identitySettings.RequireNonAlphanumeric,
                RequireUppercase = _identitySettings.RequireUppercase
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength; i++) {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }


    }
}
