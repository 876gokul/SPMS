using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProjectManagementSystem.Models
{
    public class LoginModel
    {
        [DisplayName("Name")]
        [Required]
        public string Username { get; set; }
        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}
