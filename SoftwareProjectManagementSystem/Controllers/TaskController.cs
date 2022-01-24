using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftwareProjectManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;
using Task = SoftwareProjectManagementSystem.Models.Task;

namespace SoftwareProjectManagementSystem.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin,Project Manager,Team Leader")]
    public class TaskController : Controller
    {
        private readonly testContext db;
        // Constructor
        public TaskController(testContext db)
        {
            this.db = db;
        }

        // Helper Functions
        public void DropdownLoader()
        {
            ViewBag.Status = new SelectList(db.Statuses.ToList(), "Id", "Name");
            ViewBag.Priority = new SelectList(db.Priorities.ToList(), "Id", "Name");
            ViewBag.Project = new SelectList(db.Projects.ToList(), "Id", "Name");
            ViewBag.AssignedTo = new SelectList(db.Users.ToList().Where(u => u.Role == 4 || u.Role == 5), "Id", "Name");
            ViewBag.CreatedBy = new SelectList(db.Users.ToList().Where(u => u.Role == 1 || u.Role == 2 || u.Role == 3), "Id", "Name");
        }

        // GET: Task/Index
        [HttpGet]
        public async Task<IActionResult> Index(string searchBy, string search, int pageNumber = 1)
        {
            var tasks = HelperClass.taskListWithInclude(db);
            if (searchBy == "ProjectName")
            {
                tasks = tasks.Where(t => t.ProjectNavigation.Name.StartsWith(search) || search == null);
            }
            else
            {
                tasks = tasks.Where(t => t.Name.StartsWith(search) || search == null);
            }
            return View(await PagingList<Task>.CreateAsync(tasks, pageNumber, 4));
        }

        // GET: Task/Create
        [HttpGet]
        public IActionResult Create()
        {
            
            DropdownLoader();
            return View();
        }

        // POST: Task/Create
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Task task)
        {
            
            DropdownLoader();
            if (ModelState.IsValid)
            {
                await db.Tasks.AddAsync(task);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Task/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null) return NotFound();
            Models.Task task = await HelperClass.taskWithInclude(db, (int)id);
            if (task == null) return NotFound();
            return View(task);
        }

        // GET: Task/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            
            DropdownLoader();
            if (id == null) return NotFound();
            var task = await HelperClass.taskWithInclude(db, (int)id);
            if (task == null) return NotFound();
            return View(task);
        }

        // POST: Task/Edit/5
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Task task)
        {
            DropdownLoader();
            if (id != task.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Tasks.Update(task);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Task/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            
            DropdownLoader();
            if (id == null) return NotFound();
            var task = await HelperClass.taskWithInclude(db, (int)id);
            if (task == null) return NotFound();
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await HelperClass.taskWithInclude(db, (int)id);
            db.Tasks.Remove(task);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // returns true if task is found
        // returns false if task is not found
        private bool TaskExists(int id)
        {
            return db.Tasks.Any(e => e.Id == id);
        }

    }
}
