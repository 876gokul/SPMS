using System;
using System.Collections.Generic;

#nullable disable

namespace SoftwareProjectManagementSystem.Models
{
    public partial class Client
    {
        public Client()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
