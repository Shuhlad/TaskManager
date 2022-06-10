using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.Models.Data;
using TaskManager.Models.ViewModels;
using Task = TaskManager.Models.Task;
using TaskStatus = TaskManager.Models.TaskStatus;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskContext _db;

        public TasksController(TaskContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        [ActionName("AddTask")]
        public IActionResult Add(int id)
        {
            ViewBag.ProjectId = id;
            var viewModel = new AddTaskViewModel
            {
                ProjectId = id
            };
            return View(viewModel);
        }
        
        [HttpPost]
        [ActionName("AddTask")]
        public async Task<IActionResult> Add(AddTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = new Task()
                {
                    Name = model.Name,
                    Description = model.Description,
                    CreateDate = DateTime.Now,
                    Priority = model.Priority,
                    ProjectId = model.ProjectId,
                    Project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == model.ProjectId),
                    Status = TaskStatus.ToDo
                    
                };
                _db.Tasks.Add(task);
                await _db.SaveChangesAsync();
                return RedirectToAction("ProjectDetail", "Projects",new {id = task.ProjectId});
            }
            return View("AddTask",model);
        }
    }
}