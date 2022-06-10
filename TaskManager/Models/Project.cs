using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public enum ProjectStatus 
    {
        NotStarted,
        Active,
        Completed
    }
    public class Project
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(25,ErrorMessage = "Max 25 symbols")]
        public string Name { get; set; }
        public string Description { get; set; }
        
        [Required]
        [Range(1,10)]
        public int Priority { get; set; }
        
        public ProjectStatus Status { get; set; }
        
        public DateTime CreateDate { get; set; } 
        public DateTime? FinishDate { get; set; }     
    }
}