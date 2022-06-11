using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Project> projects = _db.Projects;
            ViewBag.IdSort = sortOrder==SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewBag.NameSort = sortOrder==SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewBag.PrioritySort = sortOrder == SortState.PriorityAsc ? SortState.PriorityDesc : SortState.PriorityAsc;
            ViewBag.StatusSort = sortOrder == SortState.StatusAsc ? SortState.StatusDesc : SortState.StatusAsc;
            ViewBag.CreateDateSort = sortOrder == SortState.CreateDateAsc ? SortState.CreateDateDesc : SortState.CreateDateAsc;

            switch (sortOrder)
            {
                case SortState.IdDesc:
                    projects = projects.OrderByDescending(s => s.Name);
                    break;
                case SortState.NameDesc:
                    projects = projects.OrderByDescending(s => s.Name);
                    break;
                case SortState.NameAsc:
                    projects = projects.OrderBy(s => s.Name);
                    break;
                case SortState.PriorityAsc:
                    projects = projects.OrderBy(s => s.Priority);
                    break;
                case SortState.PriorityDesc: 
                    projects = projects.OrderByDescending(s => s.Priority);
                    break;
                case SortState.StatusAsc:
                    projects = projects.OrderBy(s => s.Status);
                    break;
                case SortState.StatusDesc:
                    projects = projects.OrderByDescending(s => s.Status);
                    break;
                case SortState.CreateDateAsc:
                    projects = projects.OrderBy(s => s.CreateDate);
                    break;
                case SortState.CreateDateDesc:
                    projects = projects.OrderByDescending(s => s.CreateDate);
                    break;
                default:
                    projects = projects.OrderBy(s => s.Name);
                    break;
            }
            
            return View( await projects.AsNoTracking().ToListAsync());
        }
        
        
        [HttpGet]
        [ActionName("ProjectDetail")]
        public IActionResult Detail(int id)
        {
            var project = _db.Projects.FirstOrDefault(t => t.Id == id);
            if (project is null)
                return NotFound();
            var tasks = _db.Tasks.AsSplitQuery().Where(t => t.ProjectId == id).ToList();
            var model = new ProjectDetailAndTasksViewModel()
            {
                Project = project,
                Tasks = tasks
            };
            ViewBag.ProjectId = project.Id;
            return View(model);
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

        [HttpGet]
        [ActionName("EditProject")]
        public IActionResult Edit(int id)
        {
            var project = _db.Projects
                .FirstOrDefault(t => t.Id == id);
            if (project is null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ActionName("EditProject")]
        public IActionResult Edit(Project project)
        {
            if (project is null)
                return NotFound();

            _db.Projects.Update(project);
            _db.SaveChanges();
            return RedirectToAction("ProjectDetail","Projects",new {id = project.Id});
        }
        [ActionName("DeleteProject")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var project = _db.Projects.FirstOrDefault(t => t.Id == id);
            if (project is null)
                return NotFound();
            _db.Projects.Remove(project);
            _db.SaveChanges();
            return RedirectToAction("ProjectDetail","Projects",new {id = project.Id});
        }
    }
}