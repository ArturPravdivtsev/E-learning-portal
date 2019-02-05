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

            // ������� ��� ����
            var role1 = new IdentityRole { Name = "student" };
            var role2 = new IdentityRole { Name = "teacher" };

            // ��������� ���� � ��
            roleManager.Create(role1);
            roleManager.Create(role2);

            // ������� �������������
            /*            var admin = new ApplicationUser { Email = "somemail@mail.ru", UserName = "somemail@mail.ru" };
                        string password = "ad46D_ewr3";
                        var result = userManager.Create(admin, password);

                        // ���� �������� ������������ ������ �������
                        if (result.Succeeded)
                        {
                            // ��������� ��� ������������ ����
                            userManager.AddToRole(admin.Id, role1.Name);
                            userManager.AddToRole(admin.Id, role2.Name);
                        }
            */

            Material material = new Material
            {
                Name = "���� 1",
                Subject = "���� ������",
                Course = 2,
                Department = "�������� �������",
                Faculty = "������������",
                //Content = fillContent(@"E:\�����\__���� � �� (�����)\������\1.doc"),
                //FileName = @"E:\�����\���� ������\__���� ������\������\���� 1 ��������.doc",
            };
            context.Materials.Add(material);
            material = new Material
            {
                Name = "���� 2",
                Subject = "���� ������",
                Course = 3,
                Department = "���������� ���������",
                Faculty = "����������� ������",
                //Content = fillContent(@"E:\�����\__���� � �� (�����)\������\1.doc"),
                //FileName = @"E:\�����\���� ������\__���� ������\������\���� 2 �������� �������.doc",
            };
            context.Materials.Add(material);
            material = new Material
            {
                Name = "���� 3",
                Subject = "���� ������",
                Course = 4,
                Department = "������",
                Faculty = "������, ���������� � �����������",
                //Content = fillContent(@"E:\�����\__���� � �� (�����)\������\1.doc"),
                //FileName = @"E:\�����\���� ������\__���� ������\������\���� 3 ������ ������.doc",
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
