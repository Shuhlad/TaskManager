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

        [HttpGet]
        [ActionName("EditTask")]
        public IActionResult Edit(int id)
        {
            var task = _db.Tasks
                .FirstOrDefault(t => t.Id == id);
            if (task is null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ActionName("EditTask")]
        public IActionResult Edit(Task task)
        {
            if (task is null)
                return NotFound();

            _db.Tasks.Update(task);
            _db.SaveChanges();
            return RedirectToAction("ProjectDetail","Projects", new {id = task.ProjectId});
        }

        [HttpGet]
        [ActionName("DetailTask")]
        public IActionResult Detail(int id)
        {
            var task = _db.Tasks
                .FirstOrDefault(t => t.Id == id);
            if (task is null)
            {
                return NotFound();
            }
            return View(task);
        }

        public IActionResult ToInProgress(int id)
        {
            var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
            if (task is null)
                return NotFound();
            if (task.Status == TaskStatus.ToDo)
            {
                task.Status = TaskStatus.InProgress;
                task.InProgressDate = DateTime.Now;
                _db.SaveChanges();
            }
            else
            {
                ViewBag.ErrorMessage = "Only new task can be started";
            }
            return RedirectToAction("ProjectDetail","Projects", new {id = task.ProjectId});
        }
        
        public IActionResult ToDone(int id)
        {
            var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
            if (task is null)
                return NotFound();
            
            if (task.Status == TaskStatus.InProgress)
            {
                task.Status = TaskStatus.Done;
                task.DoneDate = DateTime.Now;
                _db.SaveChanges();
            }
            else
            {
                ViewBag.ErrorMessage = "Only started task can be finished";
            }
            
            return RedirectToAction("ProjectDetail","Projects",new {id = task.ProjectId});
        }
        
        
        public IActionResult Delete(int id)
        {
            var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
            if (task is null)
                return NotFound();
            if (task.Status is TaskStatus.ToDo or TaskStatus.Done)
            {
                _db.Tasks.Remove(task);
                _db.SaveChanges();
                
            }
            else
            {
                ViewBag.ErrorMessage = "Only new and finished task can be deleted";
            }
            return RedirectToAction("ProjectDetail","Projects",new {id = task.ProjectId});
        }
        
    }
}