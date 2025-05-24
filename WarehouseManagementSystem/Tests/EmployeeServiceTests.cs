using DAL;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class EmployeeServiceTests
    {
        private EmployeeService service;
        private Mock<IEmployeeRepository> repo;
        private Mock<IAddressService> address;
        private Mock<IWarehouseService> warehouse;

        [TestInitialize]
        public void Init()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionStrings:DefaultConnection", "FakeConnectionString"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var db = new DbHelper(configuration);

            repo = new Mock<IEmployeeRepository>();
            address = new Mock<IAddressService>();
            warehouse = new Mock<IWarehouseService>();

            service = new EmployeeService(address.Object, warehouse.Object, repo.Object, db);
        }

        [TestMethod]
        public async Task AuthenticateEmployee_ReturnsNull_IfNotFound()
        {
            repo.Setup(x => x.GetByEmailAsync("email")).ReturnsAsync((Employee)null);

            var result = await service.AuthenticateEmployeeAsync("email", "pass");

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AuthenticateEmployee_ReturnsEmployee_IfPasswordCorrect()
        {
            var emp = new Employee("name", "email", PasswordHasher.Hash("pass"), "phone");
            repo.Setup(x => x.GetByEmailAsync("email")).ReturnsAsync(emp);

            var result = await service.AuthenticateEmployeeAsync("email", "pass");

            Assert.IsNotNull(result);
            Assert.AreEqual("name", result.Name);
        }

        [TestMethod]
        public async Task CreateEmployee_CallsAddAsync_WithHashedPassword()
        {
            var emp = new Employee("name", "email", "raw", "phone", Role.Worker, true, 1);

            await service.CreateAsync(emp);

            repo.Verify(x => x.AddAsync(It.Is<Employee>(e =>
                e.Email == "email" && PasswordHasher.Verify("raw", e.Password)
            )), Times.Once);
        }

        [TestMethod]
        public async Task UpdateRole_CallsRepoWithCorrectData()
        {
            await service.UpdateRoleAsync(2, Role.Manager);

            repo.Verify(x => x.UpdateRoleAsync(2, Role.Manager), Times.Once);
        }

        [TestMethod]
        public async Task DeleteEmployee_CallsRepo()
        {
            await service.DeleteByIdAsync(3);

            repo.Verify(x => x.DeleteByIdAsync(3), Times.Once);
        }
    }
}