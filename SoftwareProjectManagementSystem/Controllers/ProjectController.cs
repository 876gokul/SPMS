using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftwareProjectManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProjectManagementSystem.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin,Project Manager,Team Leader")]
    public class ProjectController : Controller
    {
        private readonly testContext db;
        // Constructor
        public ProjectController(testContext db)
        {
            this.db = db;
        }

        // Helper Functions for populating chart data
        private void chartdata(Project project)
        {
            ViewBag.todo = project.Tasks.Where(t => t.Status == 1).Count();
            ViewBag.onprogress = project.Tasks.Where(t => t.Status == 2).Count();
            ViewBag.done = project.Tasks.Where(t => t.Status == 3).Count();
            ViewBag.total = project.Tasks.Count();
            ViewBag.amount = project.PlannedAmount;
            ViewBag.spent = project.Tasks.Sum(t => t.Cost);
            ViewBag.left = ViewBag.amount - ViewBag.spent;
        }
        
        // Helper function for select list dropdown
        private void DropDown()
        {
            ViewBag.Users = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
            ViewBag.Clients = new SelectList(db.Clients.ToList(), "Id", "Name");
        }
        
        // GET: Project/Index
        [HttpGet]
        public async Task<IActionResult> Index(string searchBy, string search, int pageNumber = 1)
        {
            var projects = HelperClass.projectListWithInclude(db);
            if (searchBy == "CreatedBy")
            {
                projects = projects.Where(p => p.CreatedByNavigation.Name.StartsWith(search) || search == null);
            }
            else
            {
                projects = projects.Where(p => p.Name.StartsWith(search) || search == null);
            }
            return View(await PagingList<Project>.CreateAsync(projects, pageNumber, 4));
        }

        // GET: Project/Create
        [HttpGet]
        public IActionResult Create()
        {
            
            DropDown();
            return View();
        }

        // POST: Project/Create
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            DropDown();
            if (ModelState.IsValid)
            {
                await db.Projects.AddAsync(project);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Project/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null) return NotFound();
            var project = await HelperClass.projectWithInclude(db, (int)id);
            if (project == null) return NotFound();
            else chartdata(project);
            return View(project);
        }

        // GET: Project/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            
            DropDown();
            if (id == null) return NotFound();
            var project = await HelperClass.projectWithInclude(db, (int)id);
            if (project == null) return NotFound();
            return View(project);
        }

        // POST: Project/Edit/5
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            DropDown();
            if (id != project.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Projects.Update(project);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Project/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            
            DropDown();
            if (id == null) return NotFound();
            var project = await HelperClass.projectWithInclude(db, (int)id);
            if (project == null) return NotFound();
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await HelperClass.projectWithInclude(db, (int)id);
            db.Projects.Remove(project);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // returns true if project found
        // returns false if project is not found
        private bool ProjectExists(int id)
        {
            return db.Projects.Any(e => e.Id == id);
        }

    }
}
