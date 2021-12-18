using SoftwareProjectManagementSystem.Models;
using SoftwareProjectManagementSystem.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = SoftwareProjectManagementSystem.MyModels.Task;

namespace SoftwareProjectManagementSystem.Data
{
    public class DashBoardUserData
    {
        public List<Project> projectData;
        public List<User> userData;
        public List<Client> clientData;
        public List<Role> roleData;
        public List<Task> taskData;
        public DashBoardUserData(List<Project> projectData, List<User> userData, List<Client> clientData, List<Role> roleData,List<Task> taskData)
        {
            this.projectData = projectData;
            this.userData = userData;
            this.clientData = clientData;
            this.roleData = roleData;
            foreach (var user in userData)
            {
                user.Projects = projectData;

                foreach (var role in roleData)
                {
                    if(user.Role == role.Id)
                    {
                        user.RoleNavigation = role;
                    }
                }
            }
            foreach (var task in taskData)
            {
                foreach(var user in userData)
                {
                    if(user.Id == task.CreatedBy)
                    {
                        task.CreatedByNavigation = user;
                    }
                    if(user.Id == task.AssignedTo)
                    {
                        task.AssignedToNavigation = user;
                    }                
                }
            }
            foreach (var task in taskData)
            {
                foreach(var project in projectData)
                {
                    if(project.Id == task.Project)
                    {
                        task.ProjectNavigation = project;
                    }             
                }
            }
            foreach (var project in projectData)
            {
                foreach (var user in userData)
                {
                    if(project.CreatedBy == user.Id)
                    {
                        project.CreatedByNavigation = user;
                    }
                }
            }
        }
    }
}
