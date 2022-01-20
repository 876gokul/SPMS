using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareProjectManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SoftwareProjectManagementSystem.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly testContext db;

        public AccountController(testContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var users = db.Users.Include("RoleNavigation");
            User user = null;
            if (ModelState.IsValid)
            {
                user = users.FirstOrDefault(u => (u.Name == model.Username && u.Password == model.Password.Sha256()));
            }
            if (user == null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Name),
                new Claim(ClaimTypes.Role, user.RoleNavigation.Role1),
                new Claim("Email", user.Email),
                new Claim("Employee Id", user.EmployeeId),
                new Claim("Date of Birth", user.DateOfBirth.ToShortDateString()),
                new Claim("Phone", user.Phone)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = model.RememberLogin });

            return LocalRedirect(model.ReturnUrl);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
