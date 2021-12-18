using System;
using System.Collections.Generic;

#nullable disable

namespace SoftwareProjectManagementSystem.MyModels
{
    public partial class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
