using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SoftwareProjectManagementSystem.Models
{
    public partial class User
    {
        public User()
        {
            Projects = new HashSet<Project>();
            TaskAssignedToNavigations = new HashSet<Task>();
            TaskCreatedByNavigations = new HashSet<Task>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [DisplayName("Name")]
        [Required]
        [RegularExpression(
            @"^(?!.*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$",
            ErrorMessage = "Enter a valid Name. Name must not contain any special character or numbers"
         )]
        public string Name { get; set; }


        [DisplayName("Employee Id")]
        [Required]
        [RegularExpression(
            @"^(ACE)[0-9]{4}$",
            ErrorMessage = "Enter a valid Employee Id. A Valid Id Must begin with 'ACE' followed by 4 digits"
        )]
        public string EmployeeId { get; set; }


        [DisplayName("Email")]
        [Required]
        [RegularExpression(
            @"^([0-9a-zA-Z.]){3,}@[a-zA-z]{3,}(.[a-zA-Z]{2,}[a-zA-Z]*){0,}$",
            ErrorMessage = "Enter a valid Email address"
        )]
        public string Email { get; set; }


        [DisplayName("Mobile Number")]
        [Required]
        [RegularExpression(
            @"^[6-9][0-9]{9}$",
            ErrorMessage = "Mobile number Must begin with a number between 6 and 9 and should have exactly 10 digits"
        )]
        public string Phone { get; set; }


        [DisplayName("Date of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        
        [Required]
        public string Password { get; set; }


        [DisplayName("Employee Role")]
        [Required]
        public int Role { get; set; }

        
        [ForeignKey("Role")]
        public virtual Role RoleNavigation { get; set; }
        
        
        public virtual ICollection<Project> Projects { get; set; }

        
        public virtual ICollection<Task> TaskAssignedToNavigations { get; set; }
        public virtual ICollection<Task> TaskCreatedByNavigations { get; set; }
    }
}
