using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models.Data
{
    public class TaskContext : DbContext
    {
        public DbSet<Task> Tasks {get; set;}
        public DbSet<Task> Projects {get; set;}
        
        public TaskContext(DbContextOptions options) : base(options)
        {
        }
    }
}