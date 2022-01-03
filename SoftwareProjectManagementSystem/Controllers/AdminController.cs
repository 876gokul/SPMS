using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareProjectManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = SoftwareProjectManagementSystem.Models.Task;

namespace SoftwareProjectManagementSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly testContext db;
        public AdminController(testContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            ViewBag.user = HttpContext.User;
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Kanban(int projectId)
        {
            var tasks = db.Tasks.Where(t => t.Project == projectId)
                .Include("AssignedToNavigation")
                .Include("CreatedByNavigation")
                .Include("PriorityNavigation")
                .Include("ProjectNavigation")
                .Include("StatusNavigation");
            return View(tasks); 
        }
        public IActionResult Clients()
        {
            return RedirectToAction("Index", "Client");
        }
        public IActionResult Users()
        {
            return RedirectToAction("Index", "User");
        }
        public IActionResult Projects()
        {
            return RedirectToAction("Index", "Project");
        }
        public IActionResult Tasks()
        {
            return RedirectToAction("Index", "Task");
        }
    }
}
