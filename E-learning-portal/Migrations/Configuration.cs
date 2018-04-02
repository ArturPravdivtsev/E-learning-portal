namespace E_learning_portal.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.MyModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<E_learning_portal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "E_learning_portal.Models.ApplicationDbContext";
        }

        public byte[] fillContent(string filename)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.Open))
            {
                byte[] Content = new byte[fs.Length];
                fs.Read(Content, 0, Content.Length);
                return Content;
            }

        }

        protected override void Seed(E_learning_portal.Models.ApplicationDbContext context)
        {

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "student" };
            var role2 = new IdentityRole { Name = "teacher" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);

            // создаем пользователей
            /*            var admin = new ApplicationUser { Email = "somemail@mail.ru", UserName = "somemail@mail.ru" };
                        string password = "ad46D_ewr3";
                        var result = userManager.Create(admin, password);

                        // если создание пользователя прошло успешно
                        if (result.Succeeded)
                        {
                            // добавляем для пользователя роль
                            userManager.AddToRole(admin.Id, role1.Name);
                            userManager.AddToRole(admin.Id, role2.Name);
                        }
            */

            Material material = new Material
            {
                Name = "Тема 1",
                Subject = "Базы данных",
                Course = 2,
                Department = "Всеобщей истории",
                Faculty = "Исторический",
                //Content = fillContent(@"E:\учеба\__ОПИС и БД (МОАИС)\Лекции\1.doc"),
                //FileName = @"E:\учеба\Базы данных\__Базы данных\Лекции\Тема 1 Введение.doc",
            };
            context.Materials.Add(material);
            material = new Material
            {
                Name = "Тема 2",
                Subject = "Базы данных",
                Course = 3,
                Department = "Английской филологии",
                Faculty = "Иностранных языков",
                //Content = fillContent(@"E:\учеба\__ОПИС и БД (МОАИС)\Лекции\1.doc"),
                //FileName = @"E:\учеба\Базы данных\__Базы данных\Лекции\Тема 2 Основные понятия.doc",
            };
            context.Materials.Add(material);
            material = new Material
            {
                Name = "Тема 3",
                Subject = "Базы данных",
                Course = 4,
                Department = "ПОиАИС",
                Faculty = "Физики, математики и информатики",
                //Content = fillContent(@"E:\учеба\__ОПИС и БД (МОАИС)\Лекции\1.doc"),
                //FileName = @"E:\учеба\Базы данных\__Базы данных\Лекции\Тема 3 Модели данных.doc",
            };
            context.Materials.Add(material);
            context.SaveChanges();

            base.Seed(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
