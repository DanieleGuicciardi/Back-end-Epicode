using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using spiegazione_REST_epicode.Settings;
using Microsoft.Extensions.Options;
using spiegazione_REST_epicode.DTO;
using spiegazione_REST_epicode.Models;


namespace spiegazione_REST_epicode.Controllers {
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly Jwt _jwtSettings;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IOptions<Jwt> jwtSettings) {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._jwtSettings = jwtSettings.Value;

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDto) {
            try {
                if (ModelState.IsValid) {

                    ApplicationUser user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

                    if (user == null || user.IsDeleted) {
                       
                        return BadRequest();
                    }

                    var result = await _signInManager.PasswordSignInAsync(user, loginRequestDto.Password, false, false);

                    if (!result.Succeeded) {
                       
                        return BadRequest();
                    }

                    
                    

                    var roles = await _signInManager.UserManager.GetRolesAsync(user);

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    foreach (var role in roles) {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtSettings.SecurityKey));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expiry = DateTime.Now.AddMinutes(System.Convert.ToInt32(this._jwtSettings.ExpiresInMinutes));

                    var token = new JwtSecurityToken(
                        _jwtSettings.Issuer,
                        _jwtSettings.Audience,
                        claims,
                        expires: expiry,
                        signingCredentials: creds
                    );
                    string t = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new TokenResponse() { Token = t, Expires = expiry });
                }
                return BadRequest();
            }
            catch (Exception ex) {

                return BadRequest();
            }
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDto) {
            try {
                if (ModelState.IsValid && await this._userManager.FindByEmailAsync(registerRequestDto.Email) == null) {
                    var user = new ApplicationUser {
                        Id = Guid.NewGuid().ToString(),
                        UserName = registerRequestDto.Nickname,
                        Surname = registerRequestDto.Surname,
                        Name = registerRequestDto.Name,
                        Email = registerRequestDto.Email,
                        IsDeleted = false,
                        PhoneNumber = registerRequestDto.PhoneNumber
                    };
               
                    var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
                    if (!result.Succeeded) {
                        return BadRequest();
                    }
                  
                    await _userManager.AddToRolesAsync(user, new List<string> { "User" });
                    return Ok(result);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {

                return BadRequest();
            }
        }

    }
}
