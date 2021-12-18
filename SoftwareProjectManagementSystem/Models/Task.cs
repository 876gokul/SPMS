using System;
using System.Collections.Generic;

#nullable disable

namespace SoftwareProjectManagementSystem.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrption { get; set; }
        public int? Priority { get; set; }
        public int Project { get; set; }
        public int CreatedBy { get; set; }
        public int AssignedTo { get; set; }
        public int? Status { get; set; }

        public virtual User AssignedToNavigation { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual Project ProjectNavigation { get; set; }
    }
}
