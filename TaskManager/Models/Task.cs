using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public enum Status 
    {
        ToDo,
        InProgress,
        Done
    }
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public Status Status { get; set; }
        
        public int Priority { get; set; }
        
        public DateTime CreateDate { get; set; } 
        public DateTime? InProgressDate { get; set; }
        public DateTime? DoneDate { get; set; }     
    }
}