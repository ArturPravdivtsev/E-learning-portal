using E_learning_portal.Models;
using E_learning_portal.Models.MyModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_portal.Controllers.MyControllers
{
    public class MaterialController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Material(int? id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
            string ID = currentUser.Id;
            Teacher t = context.Teachers.SingleOrDefault(p => p.Id == ID);
            IEnumerable<Material> material = context.Materials.Include("Teacher");
            var selectedMaterial = from materials in material
                                   where materials.TeacherId == t.TeacherId
                                   select materials;
            return View(selectedMaterial);
        }

        [Authorize(Roles = "teacher")]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Material material)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            string ID = currentUser.Id;
            Teacher teacher = context.Teachers.SingleOrDefault(p => p.Id == ID);
            material.TeacherId = teacher.TeacherId;
            context.Materials.Add(material);
            context.SaveChanges();
            return RedirectToAction("Material");
        }

        [HttpGet]
        [Authorize(Roles = "teacher")]
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
            Material materialContext = context.Materials.SingleOrDefault(p => p.MaterialId == material.MaterialId);
            materialContext.Name = material.Name;
            materialContext.Subject = material.Subject;
            materialContext.Course = material.Course;
            materialContext.Department = material.Department;
            materialContext.Faculty = material.Faculty;
            materialContext.Fil = material.Fil;
            context.SaveChanges();

            return RedirectToAction("Material");
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
            return RedirectToAction("Material");
        }

        [Authorize(Roles = "teacher")]
        public ActionResult TeacherMaterialDetails(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Material material = context.Materials.SingleOrDefault(p => p.MaterialId == id);

            return View(material);
        }

        public ActionResult Faculty()
        {
            List<Material> list = context.Materials.ToList();
            return View(list.Distinct());
        }

        public ActionResult Department(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            List<Material> list = context.Materials.ToList().FindAll(p => p.MaterialId == id);
            return View(list.Distinct());
        }

        public ActionResult Course(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            List<Material> list = context.Materials.ToList().FindAll(p => p.MaterialId == id);
            return View(list.Distinct());
        }

        public ActionResult Subject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            List<Material> list = context.Materials.ToList().FindAll(p => p.MaterialId == id);
            return View(list.Distinct());
        }

        public ActionResult Name(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            List<Material> list = context.Materials.ToList().FindAll(p => p.MaterialId == id);
            return View(list.Distinct());
        }

        public ActionResult Content(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Material material = context.Materials.SingleOrDefault(p => p.MaterialId == id);
            //material.Fil = System.Text.Encoding.GetEncoding(1251).GetString(material.Content);

            return View(material);
        }

        public ActionResult Download(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            Material material = context.Materials.SingleOrDefault(p => p.MaterialId == id);
            string path = @"c:\Users\Archi\Desktop\" + material.Name + ".doc";

            // This text is added only once to the file.
            if (!System.IO.File.Exists(path))
            {
                // Create a file to write to.
                System.IO.File.WriteAllBytes(path, material.Content);
            }
            //using (System.IO.FileStream fs = new System.IO.FileStream(material.FileName, FileMode.OpenOrCreate))
            //{
            //    fs.Write(material.Content, 0, material.Content.Length);

            //}
            return RedirectToAction(@"Details/" + id);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
