using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Alinta.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alinta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly Context _context;

        public CustomersController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Search(string name = null)
        {
            return await _context.Customers
                .Where(c => name == null 
                    || c.FirstName.StartsWith(name, StringComparison.InvariantCultureIgnoreCase)
                    || c.LastName.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .ToArrayAsync();
        }

        [HttpPost]
        [ProducesResponseType(204)]
        public async Task<ActionResult<Customer>> Create(CustomerDto customer)
        {
            var newCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth
            };

            await _context.Customers.AddAsync(newCustomer);
            await _context.SaveChangesAsync();

            return newCustomer;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Update(int id, CustomerDto customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.DateOfBirth = customer.DateOfBirth;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class CustomerDto
        {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            public DateTime DateOfBirth { get; set; }
        }

    }
}
