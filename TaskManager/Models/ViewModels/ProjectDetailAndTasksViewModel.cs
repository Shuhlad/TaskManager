using System.Collections.Generic;

namespace TaskManager.Models.ViewModels
{
    public class ProjectDetailAndTasksViewModel
    {
        public Project Project { get; set; }
        public List<Task> Tasks { get; set; }
    }
}