using System;
using System.Collections.Generic;

#nullable disable

namespace SoftwareProjectManagementSystem.MyModels
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrption { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CreatedBy { get; set; }
        public int CreatedFor { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
