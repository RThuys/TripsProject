using backend.Data.Models;
using backend.Data.Repositories;
using backend.Data.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class BackendTripChildrenControllerTest
    {
        Mock<ITripChildRepository> TCRepo;
        [SetUp]
        public void Setup()
        {
            TCRepo = new Mock<ITripChildRepository>();
            
        }

        [Test]
        public void GetAllTripChildren()
        {
            TCRepo.Setup<Task<IEnumerable<TripChild>>>(rep => rep.GetAllTripsChildren()).Returns(Task.FromResult<IEnumerable<TripChild>>(GetTestTripChilren()));
            var tripChildren = TCRepo.Object.GetAllTripsChildren();

            List<TripChild> list = new List<TripChild>();
            foreach(TripChild trip in tripChildren.Result)
            {
                list.Add(trip);
            }

            Assert.NotNull(TCRepo);
            Assert.IsTrue(list.Count == 3);
        }

        [Test]
        public void GetTripChildById()
        {
            TripChild tripChild = GetTestTripChilren()[0];
            TCRepo.Setup<Task<TripChild>>(rep => rep.GetTripChildById(1)).Returns(Task.FromResult<TripChild>(tripChild));
            var tripChildren = TCRepo.Object.GetTripChildById(1);

            var tc2 = tripChildren.Result;

            Assert.NotNull(TCRepo);
            Assert.True(tc2.Id == 1);
        }

        [Test]
        public void GetTripChildByChildId()
        {
            Child child = GetTestChildren()[0];
            TCRepo.Setup<Task<IEnumerable<Child>>>(rep => rep.GetTripChildrenByTripIdChildren(1)).Returns(Task.FromResult<IEnumerable<Child>>(GetTestChildren()));
            var children = TCRepo.Object.GetTripChildrenByTripIdChildren(1);

            Assert.NotNull(TCRepo);
            Assert.True(children.Id == 1);
        }

        [Test]
        public void GetTripChildByTripId()
        {
            TripChild tripChild = GetTestTripChilren()[0];
            TCRepo.Setup<Task<IEnumerable<TripChild>>>(rep => rep.GetTripChildrenByTripId(1)).Returns(Task.FromResult<IEnumerable<TripChild>>(GetTestTripChilren()));
            var tripChildren = TCRepo.Object.GetTripChildrenByTripId(1);

            Assert.NotNull(TCRepo);
            Assert.True(tripChild.Id == 1);
        }

        [Test]
        public void GetTripChildByTripIdChildren()
        {
          
        }

        [Test]
        public void GetTripChildByTripIdChildrenNot()
        {

        }

        [Test]
        public void GetChildByIdNotFound()
        {
            TripChild tripChild = GetTestTripChilren()[0];
            TCRepo.Setup<Task<TripChild>>(rep => rep.GetTripChildById(1)).Returns(Task.FromResult<TripChild>(tripChild));
            var tripChildren = TCRepo.Object.GetTripChildById(2);

            Assert.NotNull(TCRepo);
            Assert.True(tripChildren.Result == null);
        }

        [Test]
        public void AddTripChild()
        {
            TripChild tripChild = GetTestTripChilren()[0];
            TCRepo.Setup<Task<TripChild>>(rep => rep.AddTripChild(tripChild)).Returns(Task.FromResult<TripChild>(tripChild));
            var tripChildren = TCRepo.Object.AddTripChild(tripChild);

            Assert.NotNull(TCRepo);
            Assert.True(tripChildren.Result.TripId == tripChild.TripId);
        }

        [Test]
        public void DeleteChild()
        {
            TripChild tripChild = GetTestTripChilren()[0];
            TCRepo.Setup<Task>(rep => rep.RemoveTripChild(1)).Returns(Task.FromResult<TripChild>(tripChild));

            Assert.NotNull(TCRepo);
            Assert.True(TCRepo.Object.GetTripChildById(1).Result == null);
        }

        public List<TripChild> GetTestTripChilren()
        {
            var testTripChildren = new List<TripChild>();
            testTripChildren.Add(new TripChild { Id = 1, TripId = 1, ChildId = 1, Scanned = false });
            testTripChildren.Add(new TripChild { Id = 2, TripId = 1, ChildId = 2, Scanned = false });
            testTripChildren.Add(new TripChild { Id = 3, TripId = 2, ChildId = 1, Scanned = false });

            return testTripChildren;
        }

        public List<Child> GetTestChildren()
        {
            var testChildren = new List<Child>();
            testChildren.Add(new Child { Id = 1, Name = "Test1", LastName = "Test1LN", Class = "TestClass" });
            testChildren.Add(new Child { Id = 2, Name = "Test2", LastName = "Test2LN", Class = "TestClass" });
            testChildren.Add(new Child { Id = 3, Name = "Test3", LastName = "Test3LN", Class = "TestClass" });

            return testChildren;
        }

        public List<Trip> GetTestTrips()
        {
            var testTrips = new List<Trip>();
            testTrips.Add(new Trip { Id = 1, Date = new System.DateTime(), SupervisorId = 1, Title = "Zoo" }) ;
            return testTrips;
        }
    }
}