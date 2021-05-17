using Domain.Models;
using Domain.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace TestApp.DataAccessADONet.Tests.Services
{
    [TestFixture]
    public class ADOMotorServiceTest
    {
        private IMotorService<Motor> service;
        Mock<IMotorService<Motor>> mockMotorService;

        [SetUp]
        public void Setup()
        {
            mockMotorService = new Mock<IMotorService<Motor>>();
            service = mockMotorService.Object;
        }

        [Test]
        public async Task GetAllAsync_CanReturnListOfMotors()
        {
            //arrange
            int expected_amount = 3;
            //act
            mockMotorService.Setup(s => s.GetAllAsync()).ReturnsAsync(ADOSampleData.GetSampleData());
            int actual = (await service.GetAllAsync()).Count();
            //assert
            Assert.AreEqual(expected_amount, actual);
        }

        [Test]
        public async Task GetById_ReturnsMotorIfSucceeded()
        {
            //arrange
            Motor expected = ADOSampleData.GetSampleData().FirstOrDefault(m => m.Id == 1);
            //act
            mockMotorService.Setup(s => s.GetByIdAsync(expected.Id))
                .ReturnsAsync(ADOSampleData.GetSampleData().First(m => m.Id == expected.Id));
            var actual = await service.GetByIdAsync(1);
            //assert
            Assert.IsAssignableFrom<Motor>(actual);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [Test]
        public void GetById_ReturnsException()
        {
            //arrange
            int id = 3;
            var expected = new Exception($"Element id = {id} not found");
            //act
            mockMotorService.Setup(s => s.GetByIdAsync(It.IsNotIn(0, 1, 2)))
                .Throws(expected);
            Exception actual = Assert.ThrowsAsync<Exception>(async () => await service.GetByIdAsync(id));
            //assert
            Assert.That(actual.Message, Is.EqualTo(expected.Message));
        }

        [Test]
        public async Task AddNew_Succeeded()
        {
            //arrange
            Motor expected = new() { Name = "NewMotor", Type = MotorType.Electric };
            int expected_id = ADOSampleData.GetSampleData().Count();
            //act
            mockMotorService.Setup(s => s.AddNewAsync(It.IsAny<Motor>())).ReturnsAsync(ADOSampleData.AssignSmapleId(expected));
            var actual = await service.AddNewAsync(new Motor {Name= "NewMotor", Type = MotorType.Electric });
            //assert
            Assert.AreEqual(expected_id, actual.Id);
        }

        [Test]
        public async Task DeleteAsync_ReturnsIdOfDeletedMotor()
        {
            //arrange
            int expected = 2;
            //act
            mockMotorService.Setup(s => s.DeleteAsync(expected))
                .ReturnsAsync(ADOSampleData.GetSampleData().First(m => m.Id == expected).Id);
            var actual = await service.DeleteAsync(2);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeleteAsync_ReturnsException()
        {
            //arrange
            int id = 3;
            var expected = new Exception($"Motor id = {id} doesn't exist in database");
            //act
            mockMotorService.Setup(s => s.DeleteAsync(It.IsNotIn(0, 1, 2)))
                .Throws(expected);
            Exception actual = Assert.ThrowsAsync<Exception>(async () => await service.DeleteAsync(id));
            //assert
            Assert.That(actual.Message, Is.EqualTo(expected.Message));
        }

        [Test]
        public async Task UpdateAsync_ReturnsUpdatedMotor()
        {
            //arrange
            Motor expected = ADOSampleData.GetSampleData().First();
            //act
            expected.Name = "ThisNameUpdated";
            mockMotorService.Setup(s => s.UpdateAsync(It.IsAny<Motor>()))
                .ReturnsAsync(() => 
                { 
                    var m = ADOSampleData.GetSampleData().First(); 
                    m.Name = "ThisNameUpdated";
                    return m;
                });
            var actual = await service.UpdateAsync(expected);
            //assert
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}
