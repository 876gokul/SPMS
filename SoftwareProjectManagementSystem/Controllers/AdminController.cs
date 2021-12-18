using Microsoft.AspNetCore.Mvc;
using SoftwareProjectManagementSystem.Data;
using SoftwareProjectManagementSystem.Models;
using SoftwareProjectManagementSystem.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            
            IEnumerable<Project> projectData = db.Projects;
            IEnumerable<User> userData = db.Users;
            IEnumerable<Client> clientData = db.Clients;
            DashBoardData FinalData = new DashBoardData(projectData, userData, clientData);
            return View(FinalData);
        }
        public IActionResult Kanban()
        {
            return View();
        }

    }
}
