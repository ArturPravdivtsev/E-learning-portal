using E_learning_portal.Models;
using E_learning_portal.Models.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_portal.Controllers.MyControllers
{
    public class MaterialController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Create()
        {

            return View();
        }


        public ActionResult FDetails()
        {
            List<Material> list = context.Materials.ToList();
            return View(list.Distinct());
        }

        public ActionResult DDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            ApplicationDbContext context = new ApplicationDbContext();
            List<Material> list = context.Materials.ToList().FindAll(p => p.MaterialId == id);
            return View(list.Distinct());
        }

        public ActionResult CDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            ApplicationDbContext context = new ApplicationDbContext();
            List<Material> list = context.Materials.ToList().FindAll(p => p.MaterialId == id);
            return View(list.Distinct());
        }

        public ActionResult SDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            ApplicationDbContext context = new ApplicationDbContext();
            List<Material> list = context.Materials.ToList().FindAll(p => p.MaterialId == id);
            return View(list.Distinct());
        }

        public ActionResult NDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            ApplicationDbContext context = new ApplicationDbContext();
            List<Material> list = context.Materials.ToList().FindAll(p => p.MaterialId == id);
            return View(list.Distinct());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Material material = context.Materials.SingleOrDefault(p => p.MaterialId == id);
            material.Fil = System.Text.Encoding.GetEncoding(1251).GetString(material.Content);

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
