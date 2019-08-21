using backend.Data.Models;
using backend.Data.Repositories;
using backend.Data.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class BackendTripsControllerTest
    {
        Mock<ITripRepository> TRepo;
        [SetUp]
        public void Setup()
        {
            TRepo = new Mock<ITripRepository>();
            
        }

        [Test]
        public void GetAllTrips()
        {
            TRepo.Setup<Task<IEnumerable<Trip>>>(rep => rep.GetAllTrips()).Returns(Task.FromResult<IEnumerable<Trip>>(GetTestTrips()));
            var trips= TRepo.Object.GetAllTrips();

            List<Trip> list = new List<Trip>();
            foreach(Trip trip in trips.Result)
            {
                list.Add(trip);
            }

            Assert.NotNull(TRepo);
            Assert.IsTrue(list.Count == 3);
        }

        [Test]
        public void GetTripsById()
        {
            Trip trip = GetTestTrips()[0];
            TRepo.Setup<Task<Trip>>(rep => rep.GetTripById(1)).Returns(Task.FromResult<Trip>(trip));
            var tripChildren = TRepo.Object.GetTripById(1);

            var tc2 = tripChildren.Result;

            Assert.NotNull(TRepo);
            Assert.True(tc2.Id == 1);
        }


        [Test]
        public void GetChildByIdNotFound()
        {
            Trip trip = GetTestTrips()[0];
            TRepo.Setup<Task<Trip>>(rep => rep.GetTripById(1)).Returns(Task.FromResult<Trip>(trip));
            var tripChildren = TRepo.Object.GetTripById(2);

            Assert.NotNull(TRepo);
            Assert.True(tripChildren.Result == null);
        }

        [Test]
        public void AddTripChild()
        {
            Trip trip = GetTestTrips()[0];
            TRepo.Setup<Task<Trip>>(rep => rep.AddTrip(trip)).Returns(Task.FromResult<Trip>(trip));
            var trips = TRepo.Object.AddTrip(trip);

            Assert.NotNull(TRepo);
            Assert.True(trips.Result.Title == trip.Title);
        }

        [Test]
        public void DeleteChild()
        {
            Trip trip = GetTestTrips()[0];
            TRepo.Setup<Task>(rep => rep.RemoveTrip(1)).Returns(Task.FromResult<Trip>(trip));

            Assert.NotNull(TRepo);
            Assert.True(TRepo.Object.GetTripById(1).Result == null);
        }

        public List<Trip> GetTestTrips()
        {
            var testTrips = new List<Trip>();
            testTrips.Add(new Trip { Id = 1, Date = new System.DateTime(), SupervisorId = 1, Title = "Zoo" });
            testTrips.Add(new Trip { Id = 2, Date = new System.DateTime(), SupervisorId = 1, Title = "Cinema" });
            testTrips.Add(new Trip { Id = 3, Date = new System.DateTime(), SupervisorId = 1, Title = "Museum" });
            return testTrips;
        }
    }
}