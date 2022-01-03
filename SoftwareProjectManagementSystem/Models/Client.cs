using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SoftwareProjectManagementSystem.Models
{
    public partial class Client
    {
        public Client()
        {
            Projects = new HashSet<Project>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [DisplayName("Customer Name")]
        [Required]
        [RegularExpression(
            @"^(?!.*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$",
            ErrorMessage = "Enter a valid Name. Name must not contain any special character or numbers"
         )]
        public string Name { get; set; }

        [DisplayName("Company Name")]
        [Required]
        public string CompanyName { get; set; }


        [DisplayName("Company Address")]
        [Required]
        public string Address { get; set; }

        
        [DisplayName("Mobile Number")]
        [Required]
        [RegularExpression(
            @"^[6-9][0-9]{9}$",
            ErrorMessage = "Mobile number Must begin with a number between 6 and 9 and should have exactly 10 digits"
        )]
        public string Phone { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
