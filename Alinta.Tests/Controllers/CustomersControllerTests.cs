using System;
using Xunit;
using Alinta.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Alinta.Controllers;
using static Alinta.Controllers.CustomersController;

namespace Alinta.Tests
{
    public class CustomersControllerTests
    {
        private static readonly DbContextOptions<Context> ContextOptions = 
            new DbContextOptionsBuilder<Context>().UseInMemoryDatabase("Customers").Options;

        [Fact]
        public async Task Create_GivenNewCustomer_CreatesCustomer()
        {
            //arrange
            using var _context = new Context(ContextOptions);
            var controller = new CustomersController(_context);

            //act
            var newCustomer = new CustomerDto
            {
                FirstName = "Test",
                LastName = "Customer",
                DateOfBirth = DateTime.Today
            };

            await controller.Create(newCustomer);
            var existingCustomer = await _context.Customers.FindAsync(1);

            //assert
            Assert.True(existingCustomer.FirstName == "Test");
        }
    }
}
