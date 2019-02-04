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
    public static class Utils
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source != null)
            {
                foreach (T obj in source)
                {
                    return false;
                }
            }
            return true;
        }
    }

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
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
                string ID = currentUser.Id;
                Teacher t = context.Teachers.SingleOrDefault(p => p.Id == ID);

                string Course = Request.Form["txtCourse"]; string Subject = Request.Form["txtSubject"];
                string Name = Request.Form["txtName"]; string Surname = Request.Form["txtSurname"];
                string TaskName = Request.Form["txtTaskName"]; string Mark = Request.Form["txtMark"];
                string Date = Request.Form["txtDate"];
            //if (Course == null || Subject == null || Name == null || Surname == null || TaskName == null || Mark == null || Date == null)
            if(String.IsNullOrEmpty(Course)|| String.IsNullOrEmpty(Subject)|| String.IsNullOrEmpty(Name)|| String.IsNullOrEmpty(Surname)
                || String.IsNullOrEmpty(TaskName)|| String.IsNullOrEmpty(Mark)|| String.IsNullOrEmpty(Date))
                return View("AddMark");
            else
            {
                //Student student = context.Students.Select(p => p.Name == Name);
                IEnumerable<Student> students = context.Students;
                var selectedStudent = from student in students
                                      where student.Name == Name && student.Surname == Surname
                                      select student.StudentId;
                if (Utils.IsNullOrEmpty(selectedStudent))
                {
                    ViewBag.Error = "Студент не найден";
                    return View("AddMark");
                }
                else
                {
                    IEnumerable<Task> tasks = context.Tasks;
                    var selectedTask = from task in tasks
                                       where task.Subject == Subject && task.Course == Int32.Parse(Course)
                                       && task.Name == TaskName && task.TeacherId == t.TeacherId && task.StudentId == selectedStudent.Single()
                                       select task;
                    if (Utils.IsNullOrEmpty(selectedTask))
                    {
                        ViewBag.Error = "Задание не найдено";
                        return View("AddMark");
                    }
                    else
                    {
                        IEnumerable<Classbook> classbooks = context.Classbooks;
                        var selectedClassbook = from classbooc in classbooks
                                                where classbooc.Subject == Subject && classbooc.Course == Int32.Parse(Course)
                                                && classbooc.TeacherId == t.TeacherId
                                                && classbooc.StudentId == selectedStudent.Single()
                                                select classbooc;
                        if (Utils.IsNullOrEmpty(selectedClassbook))
                        {
                            Classbook classbook = new Classbook
                            {
                                ClassbookId = 1,
                                TeacherId = t.TeacherId,
                                Course = Int32.Parse(Course),
                                Subject = Subject,
                                StudentId = selectedStudent.First(),
                                Task = selectedTask.First(),
                                Mark = Int32.Parse(Mark),
                                Date = DateTime.Parse(Date)
                            };
                            context.Classbooks.Add(classbook);
                            context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Error = "Запись уже существует";
                            return View("AddMark");
                        };
                    }
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "teacher")]
        public ActionResult EditMark(int? id)
        {
                 if (id == null)
                {
                    return new HttpStatusCodeResult(404);
                }

                Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
                return View(classbook);
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