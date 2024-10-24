using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using DataScience.Models;
using BCrypt.Net;
using DataScience.DatabaseContext;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace DataScience.Controllers
{
    public class LoginController : Controller
    {
        private readonly MainDbContext _context;

        public LoginController(MainDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string username, string firstname, string lastname, string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = new Author
                {
                    UserName = username,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    Role = "Author",
                    Password = BCrypt.Net.BCrypt.HashPassword(password)
                };
                _context.Authors.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Login");
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Login() { return View(); }


        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Email == email);
            if (author != null && BCrypt.Net.BCrypt.Verify(password, author.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, author.Email),
                    new Claim(ClaimTypes.Role, author.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }

            return Unauthorized();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
