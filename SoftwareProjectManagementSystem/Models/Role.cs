﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SoftwareProjectManagementSystem.MyModels
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Role1 { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
