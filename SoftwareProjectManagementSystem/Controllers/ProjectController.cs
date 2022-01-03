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
    public class ProjectController : Controller
    {
        private readonly testContext db;
        public ProjectController(testContext db)
        {
            this.db = db;
        }
        
        // GET: Projects
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var projects = db.Projects.Include("CreatedByNavigation").Include("CreatedForNavigation");
            return View(await PagingList<Project>.CreateAsync(projects, pageNumber, 4));
        }
        // GET: Project/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await db.Projects.Include("CreatedByNavigation").Include("CreatedForNavigation").Include("Tasks")
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.todo = project.Tasks.Where(t => t.Status == 1).Count();
            ViewBag.onprogress = project.Tasks.Where(t => t.Status == 2).Count();
            ViewBag.done = project.Tasks.Where(t => t.Status == 3).Count();
            ViewBag.total = project.Tasks.Count();
            ViewBag.amount = project.PlannedAmount;
            ViewBag.spent = project.Tasks.Sum(t => t.Cost);
            ViewBag.left = ViewBag.amount - ViewBag.spent;
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
            ViewBag.Clients = new SelectList(db.Clients.ToList(), "Id", "Name");
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            ViewBag.Users = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
            ViewBag.Clients = new SelectList(db.Clients.ToList(), "Id", "Name");
            if (ModelState.IsValid)
            {
                await db.Projects.AddAsync(project);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Users = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
            ViewBag.Clients = new SelectList(db.Clients.ToList(),"Id","Name");
            var project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            ViewBag.Users = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
            ViewBag.Clients = new SelectList(db.Clients.ToList(), "Id", "Name");
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Projects.Update(project);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            return View(project);
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Users = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
            ViewBag.Clients = new SelectList(db.Clients.ToList(), "Id", "Name");
            var project = await db.Projects.Include("CreatedByNavigation").Include("CreatedForNavigation")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await db.Projects.FindAsync(id);
            db.Projects.Remove(client);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Any(e => e.Id == id);
        }
        
    }
}
