using E_learning_portal.Models;
using E_learning_portal.Models.MyModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace E_learning_portal.Controllers.MyControllers
{
    public class ClassbookController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Classbook
        [Authorize(Roles = "teacher")]
        public ActionResult Index()
        {
            List<Classbook> list = context.Classbooks.ToList();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            string ID = currentUser.Id;
            Teacher t = context.Teachers.SingleOrDefault(p => p.Id == ID);
            IEnumerable<Classbook> classbook = context.Classbooks.Where(p=>p.TeacherId == t.TeacherId);
            //var selectedClassbook = from classbooks in classbook
            //                        where classbooks.TeacherId == t.TeacherId
            //                        select classbooks;
            return View(classbook);
        }

        [Authorize(Roles = "student")]
        public ActionResult SIndex()
        {
            List<Classbook> list = context.Classbooks.ToList();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            string ID = currentUser.Id;
            Student t = context.Students.SingleOrDefault(p => p.Id == ID);
            IEnumerable<Classbook> classbook = context.Classbooks.Where(p => p.StudentId == t.StudentId);
            //var selectedClassbook = from classbooks in classbook
            //                        where classbooks.StudentId == t.StudentId
            //                        select classbooks;
            return View(classbook);
        }

        [Authorize(Roles = "teacher")]
        public ActionResult AddMark()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMarkData()
        {
            if (ModelState.IsValid)
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
                string ID = currentUser.Id;
                Teacher t = context.Teachers.SingleOrDefault(p => p.Id == ID);

                string Course = Request.Form["txtCourse"]; string Subject = Request.Form["txtSubject"];
                string Name = Request.Form["txtName"]; string Surname = Request.Form["txtSurname"];
                string TaskName = Request.Form["txtTaskName"]; string Mark = Request.Form["txtMark"];
                string Date = Request.Form["txtDate"];


                Session["Course"] = Course;
                Session["Subject"] = Subject;
                Session["Name"] = Name;
                Session["Surname"] = Surname;
                Session["TaskName"] = TaskName;
                Session["Mark"] = Mark;
                Session["Date"] = Date;


                //Student student = context.Students.Select(p => p.Name == Name);
                IEnumerable<Student> students = context.Students;
                var selectedStudent = from student in students
                                      where student.Name == Name && student.Surname == Surname
                                      select student.StudentId;
                IEnumerable<Task> tasks = context.Tasks;
                var selectedTask = from task in tasks
                                   where task.Subject == Subject && task.Course == Int32.Parse(Course)
                                   && task.Name == TaskName && task.TeacherId == t.TeacherId && task.StudentId == selectedStudent.Single()
                                   select task;
                IEnumerable<Classbook> classbooks = context.Classbooks;
                var selectedClassbook = from classbooc in classbooks
                                        where classbooc.Subject == Subject && classbooc.Course == Int32.Parse(Course)
                                        && classbooc.TeacherId == t.TeacherId
                                        && classbooc.StudentId == selectedStudent.Single()
                                        select classbooc;
                try
                {
                    var s = selectedClassbook.Single();
                }
                catch (System.InvalidOperationException)
                {
                    Classbook classbook = new Classbook
                    {
                        TeacherId = t.TeacherId,
                        Course = Int32.Parse(Course),
                        Subject = Subject,
                        StudentId = selectedStudent.Single(),
                        Task = selectedTask.Single(),
                        Mark = Int32.Parse(Mark),
                        Date = DateTime.Parse(Date)
                    };
                    context.Classbooks.Add(classbook);
                    context.SaveChanges();
                    goto m1;
                }
                ViewData["ShowAlert"] = true;
                return View("AddMark");
                m1:
                return View("Index");
            }
            return View("AddMark");
        }

        [HttpGet]
        [Authorize(Roles = "teacher")]
        public ActionResult EditMark(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(404);
                }

                Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
                SelectList students = new SelectList(context.Students, "StudentId", "Name,Surname");
                ViewBag.Students = students;
                return View(classbook);
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditMark(Classbook classbook)
        {
            if (ModelState.IsValid)
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

                string ID = currentUser.Id;
                Teacher teacher = context.Teachers.SingleOrDefault(p => p.Id == ID);
                Classbook classbookContext = context.Classbooks.SingleOrDefault(p => p.ClassbookId == classbook.ClassbookId);
                classbookContext.Course = classbook.Course;
                classbookContext.Mark = classbook.Mark;
                classbookContext.Subject = classbook.Subject;
                classbookContext.Date = classbook.Date;
                classbookContext.TeacherId = teacher.TeacherId;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult DeleteMark(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
            context.Classbooks.Remove(classbook);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "teacher")]
        public ActionResult TMarkDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            Classbook classbook = context.Classbooks.SingleOrDefault(p=>p.ClassbookId == id);
            SelectList students = new SelectList(context.Students, "StudentId", "Name,Surname");
            ViewBag.Students = students;
            return View(classbook);
        }

        [Authorize(Roles = "student")]
        public ActionResult MarkDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
            return View(classbook);
        }

        
    }
}