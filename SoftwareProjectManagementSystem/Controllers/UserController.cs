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
        // Constructor
        public UserController(testContext db)
        {
            this.db = db;
        }

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

        // GET: User/Profile
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            ViewBag.CurrentUser = await CurrentUser();
            return View();
        }
        
        // GET: User/Index
        [HttpGet][Authorize(Roles = "Admin")]
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

        // GET: User/Kanban
        [HttpGet][Authorize(Roles = "Developer,Tester")]
        public async Task<IActionResult> Kanban()
        {
            var user = await CurrentUser();
            var tasks = HelperClass.taskListWithInclude(db);
            tasks = tasks.Where(t => t.AssignedToNavigation.Id == user.Id || t.CreatedByNavigation.Id == user.Id);
             
            return View(tasks.ToList());
        }

        // GET: User/Create
        [HttpGet][Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            DropDown();
             
            return View();
        }

        // POST: User/Create
        [HttpPost][Authorize(Roles = "Admin")][ValidateAntiForgeryToken]
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
        [HttpGet][Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var user = await HelperClass.UserWithInclude(db, (int)id);
            if (user == null) return NotFound();
            
            return View(user);
        }

        // GET: User/Edit/5
        [HttpGet][Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            DropDown();
            if (id == null) return NotFound();
            var user = await db.Users.FindAsync(id);
            if (user == null) return NotFound();
            
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost][Authorize(Roles = "Admin")][ValidateAntiForgeryToken]
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
        [HttpGet][Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            DropDown();
            if (id == null) return NotFound();
            var user = await HelperClass.UserWithInclude(db, (int)id);
            if (user == null) return NotFound();
            
            return View(user);
        }

        // POST: Admin/DeleteUser/5
        [HttpPost][Authorize(Roles = "Admin")][ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // returns true if user is found
        // returns false if user is not found
        private bool UserExists(int id)
        {
            return db.Users.Any(e => e.Id == id);
        }

    }
}
