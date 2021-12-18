using Microsoft.AspNetCore.Mvc;
using SoftwareProjectManagementSystem.Data;
using SoftwareProjectManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using Task = SoftwareProjectManagementSystem.Models.Task;

namespace SoftwareProjectManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly testContext db;
        public AdminController(testContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            
            List<Project> projectData = db.Projects.ToList();
            List<User> userData = db.Users.ToList();
            List<Client> clientData = db.Clients.ToList();
            List<Role> roleData = db.Roles.ToList();
            List<Task> taskData = db.Tasks.ToList();
            DashBoardUserData FinalData = new DashBoardUserData(projectData, userData, clientData,roleData,taskData);
            return View(FinalData);
        }
        public IActionResult Kanban()
        {
            return View();
        }

    }
}
