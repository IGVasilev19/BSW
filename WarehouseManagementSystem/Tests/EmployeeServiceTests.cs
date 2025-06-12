using DAL.Interfaces;
using Domain;
using Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service;
using Service.Interfaces;
using Service.Utility;
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
            repo = new Mock<IEmployeeRepository>();
            address = new Mock<IAddressService>();
            warehouse = new Mock<IWarehouseService>();

            service = new EmployeeService(address.Object, warehouse.Object, repo.Object);
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

        [TestMethod]
        public async Task RegisterOwnerWithWarehouseAsync_ValidInputs_CallsTransactionWithHashedPassword()
        {
            var address = new Address("Netherlands", "Boxtel", "Prins", 32, 1000);
            var warehouse = new Warehouse("Main");
            var employee = new Employee("John", "john@email.com", PasswordHasher.Hash("pass123"), "123456789");

            await service.RegisterOwnerWithWarehouseAsync(address, warehouse, employee);

            repo.Verify(r => r.RegisterWithWarehouseTransactionAsync(
                It.Is<Address>(a => a == address),
                It.Is<Warehouse>(w => w == warehouse),
                It.Is<Employee>(e =>
                    e.Name == "John" &&
                    e.Email == "john@email.com" &&
                    e.PhoneNumber == "123456789" &&
                    e.Password != "pass123"
                )), Times.Once);
        }

        [TestMethod]
        public async Task RegisterOwnerWithWarehouseAsync_RepoThrowsQueryFailedException_ExceptionIsPropagated()
        {
            var address = new Address();
            var warehouse = new Warehouse();
            var employee = new Employee("a", "a", "a", "a");

            repo.Setup(r => r.RegisterWithWarehouseTransactionAsync(It.IsAny<Address>(), It.IsAny<Warehouse>(), It.IsAny<Employee>()))
                .ThrowsAsync(new QueryFailedException("fail"));

            await Assert.ThrowsExceptionAsync<QueryFailedException>(() =>
                service.RegisterOwnerWithWarehouseAsync(address, warehouse, employee));
        }

        [TestMethod]
        public async Task RegisterOwnerWithWarehouseAsync_NullEmployee_ThrowsArgumentNullException()
        {
            var address = new Address();
            var warehouse = new Warehouse();

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                service.RegisterOwnerWithWarehouseAsync(address, warehouse, null));
        }

        [TestMethod]
        public async Task RegisterOwnerWithWarehouseAsync_EmptyPassword_StillHashesAndCallsTransaction()
        {
            var employee = new Employee("x", "x", "", "x");

            await service.RegisterOwnerWithWarehouseAsync(new Address(), new Warehouse(), employee);

            repo.Verify(r => r.RegisterWithWarehouseTransactionAsync(
                It.IsAny<Address>(),
                It.IsAny<Warehouse>(),
                It.Is<Employee>(e => e.Password != "")), Times.Once);
        }

        [TestMethod]
        public async Task AuthenticateEmployee_ReturnsNull_IfPasswordIncorrect()
        {
            var emp = new Employee("name", "email", PasswordHasher.Hash("correct"), "phone");
            repo.Setup(x => x.GetByEmailAsync("email")).ReturnsAsync(emp);

            var result = await service.AuthenticateEmployeeAsync("email", "wrong");

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AuthenticateEmployee_ReturnsNull_IfEmailIsEmpty()
        {
            var result = await service.AuthenticateEmployeeAsync("", "pass");

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AuthenticateEmployee_ReturnsNull_IfPasswordIsEmpty()
        {
            var emp = new Employee("name", "email", PasswordHasher.Hash("pass"), "phone");
            repo.Setup(x => x.GetByEmailAsync("email")).ReturnsAsync(emp);

            var result = await service.AuthenticateEmployeeAsync("email", "");

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AuthenticateEmployee_EmailCaseInsensitive_Matches()
        {
            var emp = new Employee("name", "email@domain.com", PasswordHasher.Hash("pass"), "phone");
            repo.Setup(x => x.GetByEmailAsync("email@domain.com")).ReturnsAsync(emp);

            var result = await service.AuthenticateEmployeeAsync("EMAIL@domain.com", "pass");

            Assert.IsNotNull(result);
            Assert.AreEqual("name", result.Name);
        }

        [TestMethod]
        public async Task CreateEmployee_DoesNotRehashAlreadyHashedPassword()
        {
            var hashed = PasswordHasher.Hash("pass");
            var emp = new Employee("name", "email", hashed, "phone", Role.Worker, true, 1);

            await service.CreateAsync(emp);

            repo.Verify(x => x.AddAsync(It.Is<Employee>(e => e.Password == hashed)), Times.Once);
        }

        [TestMethod]
        public async Task CreateEmployee_ThrowsException_IfEmailIsNull()
        {
            var emp = new Employee("name", null, "pass", "phone", Role.Worker, true, 1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                service.CreateAsync(emp));
        }

        [TestMethod]
        public async Task CreateEmployee_ThrowsException_IfPasswordIsNull()
        {
            var emp = new Employee("name", "email", null, "phone", Role.Worker, true, 1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                service.CreateAsync(emp));
        }

    }
}