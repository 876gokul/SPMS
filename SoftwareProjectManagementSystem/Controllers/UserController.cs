using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftwareProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProjectManagementSystem.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin,Project Manager,Team Leader,Developer,Tester")]
    public class UserController : Controller
    {
        private readonly testContext db;
        // Helper Functions
        private async Task<User> CurrentUser()
        {
            Dictionary<string, string> User = new Dictionary<string, string>();
            foreach (var item in HttpContext.User.Claims)
            {
                User.Add(item.Type, item.Value);
            }
            return await HelperClass.UserWithInclude(db, Convert.ToInt32(User["Id"]));
        }
        private void DropDown()
        {
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Id", "Role1");
        }
        // Constructor
        public UserController(testContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            ViewBag.CurrentUser = await CurrentUser();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string searchBy, string search, int pageNumber = 1)
        {
            var users = HelperClass.UserListWithInclude(db);
            if (searchBy == "Role")
            {
                users = users.Where(u => u.RoleNavigation.Role1 == search || search == null);
            }
            else
            {
                users = users.Where(u => u.Name.StartsWith(search) || search == null);
            }
            return View(await PagingList<User>.CreateAsync(users, pageNumber, 4));
        }

        public async Task<IActionResult> Kanban()
        {
            var user = await CurrentUser();
            var tasks = HelperClass.taskListWithInclude(db);
            tasks = tasks.Where(t => t.AssignedToNavigation.Id == user.Id || t.CreatedByNavigation.Id == user.Id);
            return View(tasks.ToList());
        }

        // GET: User/Create
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            DropDown();
            return View();
        }

        // POST: User/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            DropDown();
            if (ModelState.IsValid)
            {
                user.Password = user.Password.Sha256();
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Details/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var user = await HelperClass.UserWithInclude(db, (int)id);
            if (user == null) return NotFound();
            return View(user);
        }

        // GET: User/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            DropDown();
            if (id == null) return NotFound();
            var user = await db.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: User/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            DropDown();
            if (id != user.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    user.Password = user.Password.Sha256();
                    db.Users.Update(user);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            DropDown();
            if (id == null) return NotFound();
            var user = await HelperClass.UserWithInclude(db, (int)id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Admin/DeleteUser/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserExists(int id)
        {
            return db.Users.Any(e => e.Id == id);
        }

    }
}
