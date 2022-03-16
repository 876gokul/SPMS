using SoftwareProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareProjectManagementSystem.ViewModels
{
    public class DashBoardData
    {
        public IQueryable<Project> projects { get; set; }
        public IQueryable<Client> clients { get; set; }
        public IQueryable<User> users { get; set; }
        public IQueryable<Task> tasks { get; set; }
        public DashBoardData(IQueryable<Project> projects ,IQueryable<Client> clients, IQueryable<User> users ,IQueryable<Task> tasks)
        {
            this.projects = projects;
            this.clients = clients;
            this.users = users;
            this.tasks = tasks;
        }
        public int NoOfProjects()
        {
            return projects.Count();
        }
        public int NoOfClients()
        {
            return clients.Count();
        }
        public int NoOfUsers()
        {
            return users.Count();
        }
        public int NoOfTasks()
        {
            return tasks.Count();
        }
        public int NoOfTasksTodo()
        {
            return tasks.Where(t => t.Status == 1).Count();
        }
        public int NoOfTasksOnGoing()
        {
            return tasks.Where(t => t.Status == 2).Count();
        }
        public int NoOfTasksDone()
        {
            return tasks.Where(t => t.Status == 3).Count();
        }
        public List<string> ProjectsName()
        {
            var result = new List<string>();
            foreach(var item in projects)
            {
                result.Add(item.Name);
            }
            return result;
        }
        public List<int> NoOfTasksInEachProjects()
        {
            var result = new List<int>();
            foreach(var item in projects)
            {
                result.Add(item.Tasks.Count());
            }
            return result;
        }

        public List<string> ClientsName()
        {
            var result = new List<string>();
            foreach (var item in clients)
            {
                result.Add(item.CompanyName);
            }
            return result;
        }
        public List<double> PlannedBudgetForEachProject()
        {
            var result = new List<double>();
            foreach (var item in projects)
            {
                result.Add(item.PlannedAmount);
            }
            return result;
        }
    }
}
