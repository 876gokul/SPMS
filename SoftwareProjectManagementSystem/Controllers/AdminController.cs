using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareProjectManagementSystem.Models;
using SoftwareProjectManagementSystem.ViewModels;

namespace SoftwareProjectManagementSystem.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin,Project Manager,Team Leader")]

    public class AdminController : Controller
    {
        private readonly testContext db;
        public AdminController(testContext db)
        {
            this.db = db;
        }
        
        [HttpGet]
        public IActionResult Dashboard()
        {
            var projects = HelperClass.projectListWithInclude(db);
            var clients = HelperClass.ClientListWithInclude(db);
            var users = HelperClass.UserListWithInclude(db);
            var tasks = HelperClass.taskListWithInclude(db);
            var data = new DashBoardData(projects,clients,users,tasks);
            return View(data);
        }
        
        [HttpGet]
        public IActionResult Clients()
        {
            return RedirectToAction("Index", "Client");
        }

        [HttpGet]
        public IActionResult Users()
        {
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult Projects()
        {
            return RedirectToAction("Index", "Project");
        }

        [HttpGet]
        public IActionResult Tasks()
        {
            return RedirectToAction("Index", "Task");
        }
    }
}
