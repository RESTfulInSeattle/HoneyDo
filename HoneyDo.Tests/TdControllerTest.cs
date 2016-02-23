using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using HoneyDo.Controllers;
using HoneyDo.Models;
using System.Web.Mvc;

//ToDo Test everything
namespace HoneyDo.Tests
{
    [TestClass]
    public class TdControllerTest
    {

        [TestMethod]
        public void Todo_DueDate_Expiration_And_Completed()
        {
            //Arrange
            var currentDay = new DateTime(2016, 2, 16);
            var todoRepository = Mock.Create<Repository>();
            Todo todo = new Todo()
            {
                Completed = true,
                Deadline = new DateTime(2016, 2, 15),
                Moredetails = "Test",
                OwnerId = 0,
                TaskName = "Test Task",
                TodoId = 1
            };

            //Act
            TdController controller = new TdController(todoRepository);
            var expired = controller.PastDue(todo, currentDay);

            //Assert
            Assert.AreEqual(false, expired);
        }

        [TestMethod]
        public void Todo_DueDate_Expiration_And_NotCompleted()
        {
            //Arrange
            var currentDay = new DateTime(2016, 2, 16);
            var todoRepository = Mock.Create<Repository>();
            Todo todo = new Todo()
            {
                Completed = false,
                Deadline = new DateTime(2016, 2, 15),
                Moredetails = "Test",
                OwnerId = 0,
                TaskName = "Test Task",
                TodoId = 1
            };

            //Act
            TdController controller=new TdController(todoRepository);
            var expired = controller.PastDue(todo,currentDay);

            //Assert
            Assert.AreEqual(true,expired);
        }

        [TestMethod]
        public void Index_Returns_All_Todoes_In_DB()
        {
            //Arrange
            var todoRepository = Mock.Create<Repository>();
            Mock.Arrange(() => todoRepository.GetAll()).
                Returns(new List<Todo>()
                {
                    new Todo
                    {
                        TodoId = 1,
                        Deadline = new DateTime(2016, 2, 25),
                        TaskName = "Test Task 1"
                    },
                    new Todo
                    {
                        TodoId = 2,
                        Deadline = new DateTime(2016, 3, 15),
                        TaskName = "Test Task 2"
                    }
                }).MustBeCalled();

            //Act
            TdController controller = new TdController(todoRepository);
            ViewResult viewResult = controller.Index();
            var model = viewResult.Model as IEnumerable<Todo>;

            //Assert
            Assert.AreEqual(2,model.Count());
        }

        [TestMethod]
        public void Details_Returns_A_Todo()
        {
            //Arrange
            int id = 1;
            var todoRepository = Mock.Create<Repository>();
            Mock.Arrange(() => todoRepository.Find(id)).
                Returns(
                    new Todo()
                    { 
                        TodoId = 1,
                        Deadline = new DateTime(2016, 2, 23),
                        TaskName = "Test Task 1"
                    }
                ).MustBeCalled();

            //Act
            TdController controller = new TdController(todoRepository);
            ViewResult viewResult = controller.Details(id) as ViewResult;
            var model = viewResult.Model as Todo;

            //Assert
            Assert.AreEqual(1,model.TodoId);

        }
    }
}
