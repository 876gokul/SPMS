using SoftwareProjectManagementSystem.Models;
using SoftwareProjectManagementSystem.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProjectManagementSystem.Data
{
    public class DashBoardProjectData
    {
        public List<Project> projectData;
        public List<User> userData;
        public List<Client> clientData;
        public List<Role> roleData;
        public DashBoardProjectData(List<Project> projectData, List<User> userData, List<Client> clientData, List<Role> roleData)
        {
            this.projectData = projectData;
            this.userData = userData;
            this.clientData = clientData;
            this.roleData = roleData;
            foreach (var user in userData)
            {
                foreach (var role in roleData)
                {
                    if(user.Role == role.Id)
                    {
                        user.RoleNavigation = role;
                    }
                }
            }

        }
    }
}
