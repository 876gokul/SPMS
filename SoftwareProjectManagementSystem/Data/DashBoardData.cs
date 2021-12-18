using SoftwareProjectManagementSystem.Models;
using SoftwareProjectManagementSystem.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProjectManagementSystem.Data
{
    public class DashBoardData
    {
        public IEnumerable<Project> projectData;
        public IEnumerable<User> userData;
        public IEnumerable<Client> clientData;

        public DashBoardData(IEnumerable<Project> projectData, IEnumerable<User> userData, IEnumerable<Client> clientData)
        {
            this.projectData = projectData;
            this.userData = userData;
            this.clientData = clientData;
        }
    }
}
