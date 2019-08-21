using backend.Data.Models;
using backend.Data.Repositories;
using backend.Data.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class BackendChilrenControllerTest
    {
        Mock<IChildRepository> childRepository;
        [SetUp]
        public void Setup()
        {
            childRepository  = new Mock<IChildRepository>();
            
        }

        [Test]
        public void GetAllChildren()
        {
            childRepository.Setup<Task<IEnumerable<Child>>>(rep => rep.GetAllChildren()).Returns(Task.FromResult<IEnumerable<Child>>(GetTestChildren()));
            var children = childRepository.Object.GetAllChildren();

            List<Child> list = new List<Child>();
            foreach(Child child in children.Result)
            {
                list.Add(child);
            }

            Assert.NotNull(childRepository);
            Assert.IsTrue(list.Count == 3);
        }

        [Test]
        public void GetChildById()
        {
            Child child = new Child { Id = 1, Name = "Test1", LastName = "Test1LN", Class = "TestClass" };
            childRepository.Setup<Task<Child>>(rep => rep.GetChildById(1)).Returns(Task.FromResult<Child>(child));
            var children = childRepository.Object.GetChildById(1);

            var child2 = children.Result;

            Assert.NotNull(childRepository);
            Assert.True(child2.Id == 1);
        }

        [Test]
        public void GetChildByIdNotFound()
        {
            Child child = new Child { Id = 1, Name = "Test1", LastName = "Test1LN", Class = "TestClass" };
            childRepository.Setup<Task<Child>>(rep => rep.GetChildById(1)).Returns(Task.FromResult<Child>(child));
            var children = childRepository.Object.GetChildById(2);

            Assert.NotNull(childRepository);
            Assert.True(children.Result == null);
        }

        [Test]
        public void AddChild()
        {
            Child child = new Child { Id = 1, Name = "AddedChild", LastName = "Test1LN", Class = "TestClass" };
            childRepository.Setup<Task<Child>>(rep => rep.AddChild(child)).Returns(Task.FromResult<Child>(child));
            var children = childRepository.Object.AddChild(child);

            Assert.NotNull(childRepository);
            Assert.True(children.Result.Name == child.Name);
        }

        [Test]
        public void DeleteChild()
        {
            Child child = new Child { Id = 1, Name = "AddedChild", LastName = "Test1LN", Class = "TestClass" };
            childRepository.Setup<Task>(rep => rep.RemoveChild(1)).Returns(Task.FromResult<Child>(child));

            Assert.NotNull(childRepository);
            Assert.True(childRepository.Object.GetChildById(1).Result == null);
        }




        public List<Child> GetTestChildren()
        {
            var testChildren = new List<Child>();
            testChildren.Add(new Child { Id = 1, Name = "Test1", LastName = "Test1LN", Class = "TestClass" });
            testChildren.Add(new Child { Id = 2, Name = "Test2", LastName = "Test2LN", Class = "TestClass" });
            testChildren.Add(new Child { Id = 3, Name = "Test3", LastName = "Test3LN", Class = "TestClass" });

            return testChildren;
        }
    }
}