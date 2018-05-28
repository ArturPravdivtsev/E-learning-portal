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
    public class StudentController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Student
        [Authorize(Roles = "student")]
        public ActionResult Index()
        {
            
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            string ID = currentUser.Id;
            Student student = new Student
            {
                Id = currentUser.Id
            };
            context.Students.Add(student);
            context.SaveChanges();
            Student t = context.Students.SingleOrDefault(p => p.Id == ID);
            return View(t);
        }

        [HttpGet]
        [Authorize(Roles = "student")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            Student student = context.Students.SingleOrDefault(p => p.StudentId == id);
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            Student StudentContext = context.Students
                .SingleOrDefault(p => p.StudentId == student.StudentId);
            StudentContext.Surname = student.Surname;
            StudentContext.Name = student.Name;
            context.SaveChanges();
            return RedirectToAction("SIndex", "Start");
        }

        [Authorize(Roles = "student")]
        public ActionResult PC(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            Student student = context.Students.Single(p => p.StudentId == id);
            return View(student);
        }
    }
}