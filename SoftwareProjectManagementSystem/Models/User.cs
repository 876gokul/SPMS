using System;
using System.Collections.Generic;

#nullable disable

namespace SoftwareProjectManagementSystem.MyModels
{
    public partial class User
    {
        public User()
        {
            Projects = new HashSet<Project>();
            TaskAssignedToNavigations = new HashSet<Task>();
            TaskCreatedByNavigations = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }

        public virtual Role RoleNavigation { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Task> TaskAssignedToNavigations { get; set; }
        public virtual ICollection<Task> TaskCreatedByNavigations { get; set; }


    }
}
