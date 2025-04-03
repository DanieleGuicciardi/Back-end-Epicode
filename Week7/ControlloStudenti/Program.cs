using System.Security.Principal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using spiegazione_REST_epicode.Data;
using spiegazione_REST_epicode.Settings;
using spiegazione_REST_epicode.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Identity>(builder.Configuration.GetSection(nameof(Identity)));
builder.Services.Configure<Jwt>(builder.Configuration.GetSection(nameof(Jwt)));


// registrare DbContext 
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
    options.SignIn.RequireConfirmedAccount =
        builder.Configuration.GetValue<bool>("Identity:RequireConfirmedAccount");
    options.Password.RequiredLength =
        builder.Configuration.GetValue<int>("Identity:RequiredLength");
    options.Password.RequireDigit =
        builder.Configuration.GetValue<bool>("Identity:RequireDigit");
    options.Password.RequireLowercase =
        builder.Configuration.GetValue<bool>("Identity:RequireLowercase");
    options.Password.RequireNonAlphanumeric =
        builder.Configuration.GetValue<bool>("Identity:RequireNonAlphanumeric");
    options.Password.RequireUppercase =
        builder.Configuration.GetValue<bool>("Identity:RequireUppercase");
})
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddDefaultTokenProviders();

builder.Services.AddAuthentication(
    options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
     .AddJwtBearer(options => {
         options.TokenValidationParameters = new TokenValidationParameters {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = builder.Configuration["Jwt:Issuer"],
             ValidAudience = builder.Configuration["Jwt:Audience"],
             IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]))
         };
     });

builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<ApplicationRole>>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();



// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.Run();





