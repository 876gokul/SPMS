using Microsoft.EntityFrameworkCore;
using SoftwareProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProjectManagementSystem
{
    public class HelperClass
    {
        public static IQueryable<Models.Task> taskListWithInclude(testContext db)
        {
            return db.Tasks
                .Include("AssignedToNavigation")
                .Include("AssignedToNavigation.RoleNavigation")
                .Include("CreatedByNavigation")
                .Include("CreatedByNavigation.RoleNavigation")
                .Include("PriorityNavigation")
                .Include("ProjectNavigation")
                .Include("ProjectNavigation.CreatedByNavigation")
                .Include("ProjectNavigation.CreatedForNavigation")
                .Include("StatusNavigation");
        }
        public static async Task<Models.Task> taskWithInclude(testContext db, int id)
        {
            return await db.Tasks
                .Include("AssignedToNavigation")
                .Include("CreatedByNavigation")
                .Include("PriorityNavigation")
                .Include("ProjectNavigation")
                .Include("StatusNavigation")
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public static IQueryable<Project> projectListWithInclude(testContext db)
        {
            return db.Projects
                .Include("CreatedByNavigation.RoleNavigation")
                .Include("CreatedForNavigation")
                .Include("Tasks")
                .Include("Tasks.AssignedToNavigation")
                .Include("Tasks.CreatedByNavigation")
                .Include("Tasks.PriorityNavigation")
                .Include("Tasks.StatusNavigation");
        }
        public static async Task<Project> projectWithInclude(testContext db, int id)
        {
            return await db.Projects
                .Include("CreatedForNavigation")
                .Include("CreatedByNavigation.RoleNavigation")
                .Include("Tasks")
                .Include("Tasks.AssignedToNavigation")
                .Include("Tasks.AssignedToNavigation.RoleNavigation")
                .Include("Tasks.CreatedByNavigation")
                .Include("Tasks.CreatedByNavigation.RoleNavigation")
                .Include("Tasks.PriorityNavigation")
                .Include("Tasks.StatusNavigation")
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public static IQueryable<User> UserListWithInclude(testContext db)
        {
            return db.Users
                .Include("RoleNavigation");
        }
        public static async Task<User> UserWithInclude(testContext db, int id)
        {
            return await db.Users
                .Include("RoleNavigation")
                .Include("TaskAssignedToNavigations")
                .Include("TaskAssignedToNavigations.PriorityNavigation")
                .Include("TaskAssignedToNavigations.StatusNavigation")
                .Include("TaskCreatedByNavigations.ProjectNavigation")
                .Include("TaskAssignedToNavigations.ProjectNavigation.CreatedByNavigation")
                .Include("TaskAssignedToNavigations.ProjectNavigation.CreatedForNavigation")
                .Include("Projects")
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public static IQueryable<Client> ClientListWithInclude(testContext db)
        {
            return db.Clients;
        }
        public static async Task<Client> ClientWithInclude(testContext db, int id)
        {
            return await db.Clients.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
