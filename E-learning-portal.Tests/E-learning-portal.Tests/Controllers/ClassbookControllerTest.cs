using Microsoft.VisualStudio.TestTools.UnitTesting;
using E_learning_portal.Controllers.MyControllers;
using Moq;
using E_learning_portal.Models.MyModels;
using System.Collections.Generic;

namespace E_learning_portal.Tests.Controllers
{
    [TestClass]
    class ClassbookControllerTest
    {
        [TestMethod]
        public void IndexViewModelNotNull()
        {
            // Arrange
            var mock = new Mock<IClassbookRepository>();
            mock.Setup(a => a.GetClassbookList()).Returns(new List<Classbook>());
            ClassbookController controller = new ClassbookController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void EditPostAction_ModelError()
        {
            // arrange
            string expected = "EditMark";
            var mock = new Mock<IClassbookRepository>();
            Classbook classbook = new Classbook();
            ClassbookController controller = new ClassbookController(mock.Object);
            controller.ModelState.AddModelError("Name", "Название модели не установлено");
            // act
            ViewResult result = controller.EditMark(classbook) as ViewResult;
            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void EditPostAction_RedirectToIndexView(int id)
        {
            // arrange
            string expected = "Index";
            var mock = new Mock<IClassbookRepository>();
            mock.Setup(a => a.GetClassbook(id)).Returns(new Classbook());
            Classbook classbook = new Classbook();
            ClassbookController controller = new ClassbookController(mock.Object);
            // act
            RedirectToRouteResult result = controller.EditMark(classbook) as RedirectToRouteResult;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }

        [TestMethod]
        public void EditPostAction_SaveModel(int id)
        {
            // arrange
            var mock = new Mock<IClassbookRepository>();
            mock.Setup(a => a.GetClassbook(id)).Returns(new Classbook());
            Classbook classbook = new Classbook();
            ClassbookController controller = new ClassbookController(mock.Object);
            // act
            RedirectToRouteResult result = controller.EditMark(classbook) as RedirectToRouteResult;
            // assert
            mock.Verify(a => a.Save());
        }

        [TestMethod]
        public void DeleteAction_ModelError()
        {
            // arrange
            string expected = "Index";
            var mock = new Mock<IClassbookRepository>();
            Classbook classbook = new Classbook();
            ClassbookController controller = new ClassbookController(mock.Object);
            controller.ModelState.AddModelError("Name", "Название модели не установлено");
            // act
            ViewResult result = controller.DeleteMark(classbook.ClassbookId) as ViewResult;
            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void DeleteAction_RedirectToIndexView()
        {
            // arrange
            string expected = "Index";
            var mock = new Mock<IClassbookRepository>();
            Classbook classbook = new Classbook();
            ClassbookController controller = new ClassbookController(mock.Object);
            // act
            RedirectToRouteResult result = controller.DeleteMark(classbook.ClassbookId) as RedirectToRouteResult;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }

        [TestMethod]
        public void DeleteAction_SaveModel()
        {
            // arrange
            var mock = new Mock<IClassbookRepository>();
            Classbook classbook = new Classbook();
            ClassbookController controller = new ClassbookController(mock.Object);
            // act
            RedirectToRouteResult result = controller.DeleteMark(classbook.ClassbookId) as RedirectToRouteResult;
            // assert
            mock.Verify(a => a.Delete(classbook.ClassbookId));
            mock.Verify(a => a.Save());
        }
    }
}
