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
    public class TeacherController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Teacher
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            string ID = currentUser.Id;
            Teacher teacher = new Teacher
            {
                Id = currentUser.Id
            };
            context.Teachers.Add(teacher);
            context.SaveChanges();
            Teacher t = context.Teachers.SingleOrDefault(p => p.Id == ID);
            return View(t);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            ApplicationDbContext context = new ApplicationDbContext();
            Teacher teacher = context.Teachers.SingleOrDefault(p => p.TeacherId == id);
            return View(teacher);
        }

        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Teacher TeacherContext = context.Teachers
                .SingleOrDefault(p => p.TeacherId == teacher.TeacherId);
            TeacherContext.Surname = teacher.Surname;
            TeacherContext.Name = teacher.Name;
            TeacherContext.Patronymic = teacher.Patronymic;
            TeacherContext.Department = teacher.Department;
            context.SaveChanges();
            //return RedirectToAction("PC");
            return View("PC", teacher);
        }

        public ActionResult PC(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            ApplicationDbContext context = new ApplicationDbContext();
            Teacher teacher = context.Teachers.Single(p => p.TeacherId == id);
            return View(teacher);
        }

        public ActionResult Material(int? id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            string ID = currentUser.Id;
            Teacher t = context.Teachers.SingleOrDefault(p => p.Id == ID);
            IEnumerable<Material> material = db.Materials.Include("Teacher");
            var selectedMaterial = from materials in material
                                where materials.TeacherId == t.TeacherId 
                                select materials;
            //SelectList teachers = new SelectList(db.Teachers,"TeacherId","TeacherId");
            //ViewBag.Teacher = context.teacher;
            return View(selectedMaterial);
        }

    }
}