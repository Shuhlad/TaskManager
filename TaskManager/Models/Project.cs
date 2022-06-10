using System;   

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
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int Priority { get; set; }
        
        public ProjectStatus Status { get; set; }
        
        public DateTime CreateDate { get; set; } 
        public DateTime? FinishDate { get; set; }     
    }
}