using E_learning_portal.Models;
using E_learning_portal.Models.MyModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace E_learning_portal.Controllers.MyControllers
{
    public class TaskController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Task
        public ActionResult Index()
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            string ID = currentUser.Id;
            Teacher t = context.Teachers.SingleOrDefault(p => p.Id == ID);
            return View();
        }

        public ActionResult DTask(int? id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            string ID = currentUser.Id;
            Teacher t = context.Teachers.SingleOrDefault(p => p.Id == ID);
            IEnumerable<Task> task = context.Tasks.Include("Teacher");
            var selectedTask = from tasks in task
                               where tasks.TeacherId == t.TeacherId && tasks.done
                               select tasks;
            return View(selectedTask);
        }

        public ActionResult UTask(int? id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            string ID = currentUser.Id;
            Teacher t = context.Teachers.SingleOrDefault(p => p.Id == ID);
            IEnumerable<Task> task = context.Tasks.Include("Teacher");
            var selectedTask = from tasks in task
                               where tasks.TeacherId == t.TeacherId && !tasks.done
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

        public ActionResult SUTask(int? id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            string ID = currentUser.Id;
            Student t = context.Students.SingleOrDefault(p => p.Id == ID);
            IEnumerable<Task> task = context.Tasks.Include("Student");
            var selectedTask = from tasks in task
                               where tasks.StudentId == t.StudentId && !tasks.done
                               select tasks;
            return View(selectedTask);
        }

        public ActionResult SDTask(int? id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            string ID = currentUser.Id;
            Student t = context.Students.SingleOrDefault(p => p.Id == ID);
            IEnumerable<Task> task = context.Tasks.Include("Student");
            var selectedTask = from tasks in task
                               where tasks.StudentId == t.StudentId && tasks.done
                               select tasks;
            return View(selectedTask);
        }

        public ActionResult DoneDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Task task = context.Tasks.SingleOrDefault(p => p.TaskId == id);
            return View(task);
        }

        [HttpGet]
        public ActionResult DoneEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Task task = context.Tasks.SingleOrDefault(p => p.TaskId == id);
            SelectList teachers = new SelectList(context.Teachers, "TeacherId", "Surname");
            ViewBag.Teachers = teachers;
            return View(task);
        }

        [HttpPost]
        public ActionResult DoneEdit(Task task)
        {
            Task taskContext = context.Tasks.SingleOrDefault(p => p.TaskId == task.TaskId);
            taskContext.Name = task.Name;
            taskContext.Subject = task.Subject;
            taskContext.Course = task.Course;
            taskContext.Fil = task.Fil;
            context.SaveChanges();

            return RedirectToAction("Task");
        }

        public ActionResult UndoneDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            Task task = context.Tasks.SingleOrDefault(p => p.TaskId == id);
            return View(task);
        }

        public ActionResult MakeTask(int? id)
        {
            SelectList teachers = new SelectList(context.Teachers, "TeacherId", "Name,Surname");
            ViewBag.Teachers = teachers;
            return View();
        }

        [HttpPost]
        public ActionResult MakeTask(Task task)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            string ID = currentUser.Id;
            Student student = context.Students.SingleOrDefault(p => p.Id == ID);
            task.StudentId = student.StudentId;
            context.Tasks.Add(task);
            context.SaveChanges();

            var s = context.Teachers.SingleOrDefault(p=>p.TeacherId == task.TeacherId);
            ApplicationUser tUser = UserManager.FindById(s.Id);

            var customerName = task.Teacher.Surname.ToString();
            var customerEmail = tUser.Email;
            var customerRequest = "Уважаемый,"+s.Surname+s.Name+s.Patronymic+". Студент"+student.Surname+student.Name+"выполнил задание. Необходимо зайти в портал и выполнить задание.";
            var errorMessage = "";
            var debuggingFlag = false;
            try
            {
                // Initialize WebMail helper
                WebMail.EnableSsl = true;
                WebMail.SmtpServer = "smtp.provider.com";
                WebMail.SmtpPort = 587;
                WebMail.UserName = "E-learning portal";
                WebMail.Password = "010203Deadpool";
                WebMail.From = "deadpoollyo@gmail.com";

                // Send email
                WebMail.Send(to: customerEmail,
                    subject: "Один из ваших студентов выполнил задание",
                    body: customerRequest
                );
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return View();
        }
    }
}