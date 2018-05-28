using E_learning_portal.Models;
using E_learning_portal.Models.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_portal.Controllers.MyControllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Students()
        {
            List<Student> list = context.Students.ToList();
            return View(list);
        }

        [HttpGet]
       // [Authorize(Roles = "admin")]
        public ActionResult SEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Student student = context.Students.SingleOrDefault(p => p.StudentId == id);
            return View(student);
        }

        [HttpPost]
        public ActionResult SEdit(Student student)
        {
            Student studentContext = context.Students.SingleOrDefault(p => p.StudentId == student.StudentId);
            studentContext.Name = student.Name;
            studentContext.Surname = student.Surname;
            context.SaveChanges();

            return RedirectToAction("Students");
        }

       // [Authorize(Roles = "admin")]
        public ActionResult SDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Student student = context.Students.SingleOrDefault(p => p.StudentId == id);
            return View(student);
        }

        public ActionResult SDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Student student = context.Students.SingleOrDefault(p => p.StudentId == id);
            context.Students.Remove(student);
            context.SaveChanges();
            return RedirectToAction("Students");
       }

        public ActionResult Teachers()
        {
            List<Teacher> list = context.Teachers.ToList();
            return View(list);
        }

        [HttpGet]
       // [Authorize(Roles = "admin")]
        public ActionResult TEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Teacher teacher = context.Teachers.SingleOrDefault(p => p.TeacherId == id);
            return View(teacher);
        }

        [HttpPost]
        public ActionResult TEdit(Teacher teacher)
        {
            Teacher teacherContext = context.Teachers.SingleOrDefault(p => p.TeacherId == teacher.TeacherId);
            teacherContext.Name = teacher.Name;
            teacherContext.Surname = teacher.Surname;
            teacherContext.Patronymic = teacher.Patronymic;
            teacherContext.Department = teacher.Department;
            context.SaveChanges();

            return RedirectToAction("Teachers");
        }

       // [Authorize(Roles = "admin")]
        public ActionResult TDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Teacher teacher = context.Teachers.SingleOrDefault(p => p.TeacherId == id);
            return View(teacher);
        }

        public ActionResult TDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Teacher teacher = context.Teachers.SingleOrDefault(p => p.TeacherId == id);
            context.Teachers.Remove(teacher);
            context.SaveChanges();
            return RedirectToAction("Teachers");
        }

        public ActionResult Materials()
        {
            List<Material> list = context.Materials.ToList();
            return View(list);
        }

       // [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Material material)
        {
            IEnumerable<Teacher> teacher = context.Teachers;
            var selectedTeacher = from teachers in teacher
                                   where teachers.Name == material.Teacher.Name && teachers.Surname == material.Teacher.Surname && teachers.Patronymic == material.Teacher.Patronymic
                                  select teachers.TeacherId;
            material.TeacherId = selectedTeacher.SingleOrDefault();
            context.Materials.Add(material);
            context.SaveChanges();
            return RedirectToAction("Materials");
        }

        [HttpGet]
        //[Authorize(Roles = "teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Material material = context.Materials.SingleOrDefault(p => p.MaterialId == id);
            return View(material);
        }

        [HttpPost]
        public ActionResult Edit(Material material)
        {
            IEnumerable<Teacher> teacher = context.Teachers;
            var selectedTeacher = from teachers in teacher
                                  where teachers.Name == material.Teacher.Name && teachers.Surname == material.Teacher.Surname && teachers.Patronymic == material.Teacher.Patronymic
                                  select teachers.TeacherId;
            Material materialContext = context.Materials.SingleOrDefault(p => p.MaterialId == material.MaterialId);
            materialContext.Name = material.Name;
            materialContext.Subject = material.Subject;
            materialContext.Course = material.Course;
            materialContext.Department = material.Department;
            materialContext.Faculty = material.Faculty;
            materialContext.Fil = material.Fil;
            materialContext.TeacherId = selectedTeacher.Single();
            context.SaveChanges();

            return RedirectToAction("Materials");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Material material = context.Materials.SingleOrDefault(p => p.MaterialId == id);
            context.Materials.Remove(material);
            context.SaveChanges();
            return RedirectToAction("Materials");
        }

       // [Authorize(Roles = "teacher")]
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Material material = context.Materials.SingleOrDefault(p => p.MaterialId == id);
            return View(material);
        }
    }
}