using E_learning_portal.Models;
using E_learning_portal.Models.MyModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_portal.Controllers.MyControllers
{
    public class TaskController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Task(int? id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            string ID = currentUser.Id;
            Teacher t = context.Teachers.SingleOrDefault(p => p.Id == ID);
            IEnumerable<Task> task = context.Tasks.Include("Teacher");
            var selectedTask = from tasks in task
                                   where tasks.TeacherId == t.TeacherId
                                   select tasks;
            return View(selectedTask);
        }

        public ActionResult Create()
        {
            SelectList students = new SelectList(context.Students, "StudentId", "Surname");
            ViewBag.Students = students;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            string ID = currentUser.Id;
            Teacher teacher = context.Teachers.SingleOrDefault(p => p.Id == ID);
            task.TeacherId = teacher.TeacherId;
            context.Tasks.Add(task);
            context.SaveChanges();
            return View("TaskView", task);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Task task = context.Tasks.SingleOrDefault(p => p.TaskId == id);
            SelectList students = new SelectList(context.Students, "StudentId", "Surname");
            ViewBag.Students = students;
            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            Task taskContext = context.Tasks.SingleOrDefault(p => p.TaskId == task.TaskId);
            taskContext.Name = task.Name;
            taskContext.Subject = task.Subject;
            taskContext.Course = task.Course;
            taskContext.Fil = task.Fil;
            context.SaveChanges();

            return RedirectToAction("Task");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Task task = context.Tasks.SingleOrDefault(p => p.TaskId == id);
            context.Tasks.Remove(task);
            context.SaveChanges();
            return RedirectToAction("Task");
        }

        public ActionResult TeacherTaskDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Task task = context.Tasks.SingleOrDefault(p => p.TaskId == id);

            return View(task);
        }

    }
}