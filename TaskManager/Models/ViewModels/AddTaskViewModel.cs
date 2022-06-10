using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.ViewModels
{
    public class AddTaskViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(25,ErrorMessage = "Max 25 symbols")]
        public string Name { get; set; }
        public string? Description { get; set; }
        
        public TaskStatus? Status { get; set; }
        [Required]
        [Range(1,10)]
        public int Priority { get; set; }
        
        public DateTime? CreateDate { get; set; } 
        public DateTime? InProgressDate { get; set; }
        public DateTime? DoneDate { get; set; }

        [Required(ErrorMessage = "connected project not founded")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}