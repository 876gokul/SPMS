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
        // Constructor
        public AdminController(testContext db)
        {
            this.db = db;
        }

        // GET: Admin/Dashboard
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
        
    }
}
