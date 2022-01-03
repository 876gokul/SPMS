using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SoftwareProjectManagementSystem.Models
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
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


        [DisplayName("Project Description")]
        [Required]
        public string Descrption { get; set; }


        [DisplayName("Start Date")]
        [Required]
        public DateTime StartDate { get; set; }


        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }


        [DisplayName("Created By")]
        [Required]
        public int CreatedBy { get; set; }

        [DisplayName("Created For")]
        [Required]
        public int CreatedFor { get; set; }

        [DisplayName("Planned Amount")]
        [Required]
        public double PlannedAmount { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual Client CreatedForNavigation { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
