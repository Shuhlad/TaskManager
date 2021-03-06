using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models.Data
{
    public class TaskContext : DbContext
    {
        public DbSet<Project> Projects {get; set;}
        public DbSet<Task> Tasks {get; set;}
        
        
        public TaskContext(DbContextOptions options) : base(options)
        {
        }
    }
}