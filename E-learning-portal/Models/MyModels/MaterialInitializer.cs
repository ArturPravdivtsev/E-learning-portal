using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.IO;

namespace E_learning_portal.Models.MyModels
{
    public class MaterialInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public byte[] fillContent(string filename)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.Open))
            {
                byte[] Content = new byte[fs.Length];
                fs.Read(Content, 0, Content.Length);
                return Content;
            }

        }

        protected override void Seed(ApplicationDbContext context)
        {

            Material material = new Material
            {
                Name = "Тема 1",
                Subject = "Базы данных",
                Course = 2,
                Department = "Всеобщей истории",
                Faculty = "Исторический",
                Content = fillContent(@"E:\учеба\__ОПИС и БД (МОАИС)\Лекции\1.doc"),
                FileName = @"E:\учеба\Базы данных\__Базы данных\Лекции\Тема 1 Введение.doc"
            };
            context.Materials.Add(material);
            material = new Material
            {
                Name = "Тема 2",
                Subject = "Базы данных",
                Course = 3,
                Department = "Английской филологии",
                Faculty = "Иностранных языков",
                Content = fillContent(@"E:\учеба\__ОПИС и БД (МОАИС)\Лекции\1.doc"),
                FileName = @"E:\учеба\Базы данных\__Базы данных\Лекции\Тема 2 Основные понятия.doc"
            };
            context.Materials.Add(material);
            material = new Material
            {
                Name = "Тема 3",
                Subject = "Базы данных",
                Course = 4,
                Department = "ПОиАИС",
                Faculty = "Физики, математики и информатики",
                Content = fillContent(@"E:\учеба\__ОПИС и БД (МОАИС)\Лекции\1.doc"),
                FileName = @"E:\учеба\Базы данных\__Базы данных\Лекции\Тема 3 Модели данных.doc"
            };
            context.Materials.Add(material);
            context.SaveChanges();
            base.Seed(context);

        }
    }
}