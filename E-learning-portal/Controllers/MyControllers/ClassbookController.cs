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
            IEnumerable<Classbook> classbook = context.Classbooks.Include("Teacher");
            var selectedClassbook = from classbooks in classbook
                                   where classbooks.TeacherId == t.TeacherId
                                   select classbooks;
            return View(selectedClassbook.Distinct());
        }

        public ActionResult CourseDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            List<Classbook> list = context.Classbooks.ToList().FindAll(p => p.ClassbookId == id);
            return View(list.Distinct());
        }

        [HttpGet]
        public ActionResult CourseCreate(int? id)
        {

            return View();
        }

        [HttpPost]
        public ActionResult CourseCreate(Classbook classbook)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            string ID = currentUser.Id;
            Teacher teacher = context.Teachers.SingleOrDefault(p => p.Id == ID);
            classbook.TeacherId = teacher.TeacherId;
            context.Classbooks.Add(classbook);
            context.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult CourseEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
            return View(classbook);
        }

        [HttpPost]
        public ActionResult CourseEdit(Classbook classbook)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            string ID = currentUser.Id;
            Teacher teacher = context.Teachers.SingleOrDefault(p => p.Id == ID);
            Classbook classbookContext = context.Classbooks.SingleOrDefault(p => p.ClassbookId == classbook.ClassbookId);
            classbookContext.Course = classbook.Course;
            classbookContext.TeacherId = teacher.TeacherId;
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult CourseDelete(int? id)
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

        public ActionResult SubjectDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            List<Classbook> list = context.Classbooks.ToList().FindAll(p => p.ClassbookId == id);
            return View(list.Distinct());
        }

        [HttpGet]
        public ActionResult SubjectCreate(int? id)
        {

            return View();
        }

        [HttpPost]
        public ActionResult SubjectCreate(Classbook classbook)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            string ID = currentUser.Id;
            Teacher teacher = context.Teachers.SingleOrDefault(p => p.Id == ID);
            classbook.TeacherId = teacher.TeacherId;
            context.Classbooks.Add(classbook);
            context.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult SubjectEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
            return View(classbook);
        }

        [HttpPost]
        public ActionResult SubjectEdit(Classbook classbook)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            string ID = currentUser.Id;
            Teacher teacher = context.Teachers.SingleOrDefault(p => p.Id == ID);
            Classbook classbookContext = context.Classbooks.SingleOrDefault(p => p.ClassbookId == classbook.ClassbookId);
            classbookContext.Course = classbook.Course;
            classbookContext.TeacherId = teacher.TeacherId;
            context.SaveChanges();

            return RedirectToAction("CourseDetails");
        }

        public ActionResult SubjectDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Classbook classbook = context.Classbooks.SingleOrDefault(p => p.ClassbookId == id);
            context.Classbooks.Remove(classbook);
            context.SaveChanges();
            return RedirectToAction("CourseDetails");
        }

        public ActionResult GDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            List<Classbook> list = context.Classbooks.ToList().FindAll(p => p.ClassbookId == id);
            return View(list.Distinct());
        }

        

        public ActionResult Students(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            List<Classbook> list = context.Classbooks.ToList().FindAll(p => p.ClassbookId == id);
            return View();
        }
    }
}