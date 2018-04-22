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
    public class TeacherController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Teacher
        [Authorize(Roles = "teacher")]
        public ActionResult Index()
        {
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
        [Authorize(Roles = "teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            Teacher teacher = context.Teachers.SingleOrDefault(p => p.TeacherId == id);
            return View(teacher);
        }

        [HttpPost]
        [Authorize(Roles = "teacher")]
        public ActionResult Edit(Teacher teacher)
        {
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

        [Authorize(Roles = "teacher")]
        public ActionResult PC(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            Teacher teacher = context.Teachers.Single(p => p.TeacherId == id);
            return View(teacher);
        }

    }
}