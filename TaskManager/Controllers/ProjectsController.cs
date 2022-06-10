using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Models.Data;
using TaskManager.Models.ViewModels;

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
        [HttpGet]
        [ActionName("AddProject")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("AddProject")]
        public async Task<IActionResult> Add(Project project)
        {
            if (ModelState.IsValid)
            {
                project.CreateDate = DateTime.Now;
                project.Status = ProjectStatus.NotStarted;
                _db.Projects.Add(project);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Projects");
            }
            return View("AddProject",project);
        }
        
    }
}