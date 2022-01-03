using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftwareProjectManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = SoftwareProjectManagementSystem.Models.Task;

namespace SoftwareProjectManagementSystem.Controllers
{
    public class TaskController : Controller
    {
        private readonly testContext db;
        public TaskController(testContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index(int pageNumber=1)
        {
            var tasks = db.Tasks
                .Include("AssignedToNavigation")
                .Include("CreatedByNavigation")
                .Include("PriorityNavigation")
                .Include("ProjectNavigation")
                .Include("StatusNavigation");

            return View(await PagingList<Task>.CreateAsync(tasks,pageNumber,4));
        }
        // GET: Task/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Models.Task task = await db.Tasks
                .Include("AssignedToNavigation")
                .Include("CreatedByNavigation")
                .Include("PriorityNavigation")
                .Include("ProjectNavigation")
                .Include("StatusNavigation")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            ViewBag.Status = new SelectList(db.Statuses.ToList(), "Id", "Name");
            ViewBag.Priority = new SelectList(db.Priorities.ToList(), "Id", "Name");
            ViewBag.Project = new SelectList(db.Projects.ToList(), "Id", "Name");
            ViewBag.AssignedTo = new SelectList(db.Users.ToList().Where(u => u.Role == 4 || u.Role == 5), "Id", "Name");
            ViewBag.CreatedBy = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Task task)
        {
            ViewBag.Status = new SelectList(db.Statuses.ToList(), "Id", "Name");
            ViewBag.Priority = new SelectList(db.Priorities.ToList(), "Id", "Name");
            ViewBag.Project = new SelectList(db.Projects.ToList(), "Id", "Name");
            ViewBag.AssignedTo = new SelectList(db.Users.ToList().Where(u => u.Role == 4 || u.Role == 5), "Id", "Name");
            ViewBag.CreatedBy = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
            if (ModelState.IsValid)
            {
                await db.Tasks.AddAsync(task);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Status = new SelectList(db.Statuses.ToList(), "Id", "Name");
            ViewBag.Priority = new SelectList(db.Priorities.ToList(), "Id", "Name");
            ViewBag.Project = new SelectList(db.Projects.ToList(), "Id", "Name");
            ViewBag.AssignedTo = new SelectList(db.Users.ToList().Where(u => u.Role == 4 || u.Role == 5), "Id", "Name");
            ViewBag.CreatedBy = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
            var task = await db.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Task task)
        {
            ViewBag.Status = new SelectList(db.Statuses.ToList(), "Id", "Name");
            ViewBag.Priority = new SelectList(db.Priorities.ToList(), "Id", "Name");
            ViewBag.Project = new SelectList(db.Projects.ToList(), "Id", "Name");
            ViewBag.AssignedTo = new SelectList(db.Users.ToList().Where(u => u.Role == 4 || u.Role == 5), "Id", "Name");
            ViewBag.CreatedBy = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Tasks.Update(task);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Status = new SelectList(db.Statuses.ToList(), "Id", "Name");
            ViewBag.Priority = new SelectList(db.Priorities.ToList(), "Id", "Name");
            ViewBag.Project = new SelectList(db.Projects.ToList(), "Id", "Name");
            ViewBag.AssignedTo = new SelectList(db.Users.ToList().Where(u => u.Role == 4 || u.Role == 5), "Id", "Name");
            ViewBag.CreatedBy = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
            var user = await db.Tasks
                .Include("AssignedToNavigation")
                .Include("CreatedByNavigation")
                .Include("PriorityNavigation")
                .Include("ProjectNavigation")
                .Include("StatusNavigation")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/DeleteUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await db.Tasks.FindAsync(id);
            db.Tasks.Remove(task);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Any(e => e.Id == id);
        }

    }
}
