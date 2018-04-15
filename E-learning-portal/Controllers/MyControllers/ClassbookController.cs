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
    public class ClassbookController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Classbook
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
                var s =selectedClassbook.Single();
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
                //goto m1;
            }
            //var html = string.Empty;
            //    html = "<div style='border: 1px solid red;margin-bottom:5px;'>"
            //      + "Такая запись уже существует!"
            //      + "</div>";
            //return View("AddMark",html);
            //m1:
            //return View("Index");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditMark(int? id)
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

        [HttpPost]
        public ActionResult EditMark(Classbook classbook)
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

        public ActionResult MarkDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
            return View(classbook);
        }

        //public ActionResult CourseDetails(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }
        //    List<Classbook> list = context.Classbooks.ToList().FindAll(p => p.ClassbookId == id);
        //    return View(list.Distinct());
        //}

        //[HttpGet]
        //public ActionResult CourseCreate(int? id)
        //{

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CourseCreate(Classbook classbook)
        //{
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //    ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

        //    string ID = currentUser.Id;
        //    Teacher teacher = context.Teachers.SingleOrDefault(p => p.Id == ID);
        //    classbook.TeacherId = teacher.TeacherId;
        //    context.Classbooks.Add(classbook);
        //    context.SaveChanges();
        //    return View("Index");
        //}
        //[HttpGet]
        //public ActionResult CourseEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }

        //    Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
        //    return View(classbook);
        //}

        //[HttpPost]
        //public ActionResult CourseEdit(Classbook classbook)
        //{
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //    ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

        //    string ID = currentUser.Id;
        //    Teacher teacher = context.Teachers.SingleOrDefault(p => p.Id == ID);
        //    Classbook classbookContext = context.Classbooks.SingleOrDefault(p => p.ClassbookId == classbook.ClassbookId);
        //    classbookContext.Course = classbook.Course;
        //    classbookContext.TeacherId = teacher.TeacherId;
        //    context.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        //public ActionResult CourseDelete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }

        //    Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
        //    context.Classbooks.Remove(classbook);
        //    context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //public ActionResult SubjectDetails(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }
        //    List<Classbook> list = context.Classbooks.ToList().FindAll(p => p.ClassbookId == id);
        //    return View(list.Distinct());
        //}

        //[HttpGet]
        //public ActionResult SubjectCreate(int? id)
        //{

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult SubjectCreate(Classbook classbook)
        //{
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //    ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

        //    string ID = currentUser.Id;
        //    Teacher teacher = context.Teachers.SingleOrDefault(p => p.Id == ID);
        //    classbook.TeacherId = teacher.TeacherId;
        //    context.Classbooks.Add(classbook);
        //    context.SaveChanges();
        //    return View("CourseDetails");
        //}
        //[HttpGet]
        //public ActionResult SubjectEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }

        //    Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
        //    return View(classbook);
        //}

        //[HttpPost]
        //public ActionResult SubjectEdit(Classbook classbook)
        //{
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //    ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

        //    string ID = currentUser.Id;
        //    Teacher teacher = context.Teachers.SingleOrDefault(p => p.Id == ID);
        //    Classbook classbookContext = context.Classbooks.SingleOrDefault(p => p.ClassbookId == classbook.ClassbookId);
        //    classbookContext.Course = classbook.Course;
        //    classbookContext.TeacherId = teacher.TeacherId;
        //    context.SaveChanges();

        //    return RedirectToAction("CourseDetails");
        //}

        //public ActionResult SubjectDelete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }

        //    Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
        //    context.Classbooks.Remove(classbook);
        //    context.SaveChanges();
        //    return RedirectToAction("CourseDetails");
        //}

        //public ActionResult GDetails(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }

        //    List<Classbook> list = context.Classbooks.ToList().FindAll(p => p.ClassbookId == id);
        //    return View(list.Distinct());
        //}



        //public ActionResult Students(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }
        //    List<Classbook> list = context.Classbooks.ToList().FindAll(p => p.ClassbookId == id);
        //    return View();
        //}
    }
}