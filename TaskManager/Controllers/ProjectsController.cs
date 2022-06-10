using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.Data;

namespace TaskManager.Controllers
{
    
    public class ProjectsController : Controller
    {
        private readonly TaskContext _db;

        public ProjectsController(TaskContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var projects = _db.Projects.ToList();
            return View(projects);
        }
        [HttpGet]
        [ActionName("ProjectDetail")]
        public IActionResult Detail(int id)
        {
            var project = _db.Projects.FirstOrDefault(t => t.Id == id);
            if (project is null)
                return NotFound();
            return View(project);
        }
        
    }
}