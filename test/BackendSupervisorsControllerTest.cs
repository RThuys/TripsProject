using backend.Data.Models;
using backend.Data.Repositories;
using backend.Data.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class BackendSupervisorsControllerTest
    {
        Mock<ISupervisorRepository> supervisorRepository;
        [SetUp]
        public void Setup()
        {
            supervisorRepository = new Mock<ISupervisorRepository>();
            
        }

        [Test]
        public void GetAllChildren()
        {
            supervisorRepository.Setup<Task<IEnumerable<Supervisor>>>(rep => rep.GetAllSupervisor()).Returns(Task.FromResult<IEnumerable<Supervisor>>(GetTestSupervisors()));
            var supervisors = supervisorRepository.Object.GetAllSupervisor();

            List<Supervisor> list = new List<Supervisor>();
            foreach(Supervisor supervisor in supervisors.Result)
            {
                list.Add(supervisor);
            }

            Assert.NotNull(supervisorRepository);
            Assert.IsTrue(list.Count == 3);
        }

        [Test]
        public void GetSupervisorById()
        {
            Supervisor supervisor = GetTestSupervisors()[0];
            supervisorRepository.Setup<Task<Supervisor>>(rep => rep.GetSupervisorById(1)).Returns(Task.FromResult<Supervisor>(supervisor));
            var supervisors = supervisorRepository.Object.GetSupervisorById(1);

            var sup2 = supervisors.Result;

            Assert.NotNull(supervisorRepository);
            Assert.True(sup2.Id == 1);
        }

        [Test]
        public void GetSupervisorByIdNotFound()
        {
            Supervisor supervisor = GetTestSupervisors()[0];
            supervisorRepository.Setup<Task<Supervisor>>(rep => rep.GetSupervisorById(1)).Returns(Task.FromResult<Supervisor>(supervisor));
            var supervisors = supervisorRepository.Object.GetSupervisorById(2);

            Assert.NotNull(supervisorRepository);
            Assert.True(supervisors.Result == null);
        }

        [Test]
        public void AddChild()
        {
            Supervisor supervisor = GetTestSupervisors()[0];
            supervisorRepository.Setup<Task<Supervisor>>(rep => rep.AddSupervisor(supervisor)).Returns(Task.FromResult<Supervisor>(supervisor));
            var supervisors = supervisorRepository.Object.AddSupervisor(supervisor);

            Assert.NotNull(supervisorRepository);
            Assert.True(supervisors.Result.Name == supervisor.Name);
        }

        [Test]
        public void DeleteSupervisor()
        {
            Supervisor supervisor = GetTestSupervisors()[0];
            supervisorRepository.Setup<Task>(rep => rep.RemoveSupervisor(1)).Returns(Task.FromResult<Supervisor>(supervisor));

            Assert.NotNull(supervisorRepository);
            Assert.True(supervisorRepository.Object.GetSupervisorById(1).Result == null);
        }

        public List<Supervisor> GetTestSupervisors()
        {
            var testSupervisors = new List<Supervisor>();
           testSupervisors.Add(new Supervisor { Id = 1, Name = "Test1" });
           testSupervisors.Add(new Supervisor { Id = 2, Name = "Test2" });
            testSupervisors.Add(new Supervisor { Id = 3, Name = "Test3" });

            return testSupervisors;
        }
    }
}