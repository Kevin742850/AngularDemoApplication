#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Core;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.Persistence;

namespace PharmacyManagementSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork as UnitOfWork;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomer()
        {
            return await Task.Run(() => _unitOfWork.Customers.GetAll());
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await Task.Run(() => _unitOfWork.Customers.Find(c => c.Id == id).FirstOrDefault());

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            try
            {
                Customer updatedCustomer = await Task.Run(() => _unitOfWork.Customers.Find(c => c.Id == id).FirstOrDefault());
                if (updatedCustomer == null)
                {
                    return NotFound();
                }
                else
                {                    
                    _unitOfWork.Customers.Update(customer);
                    _unitOfWork.Complete();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            await Task.Run(() => _unitOfWork.Customers.Add(customer));
            _unitOfWork.Complete();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await Task.Run(() => _unitOfWork.Customers.Find(c => c.Id == id).FirstOrDefault());
            if (customer == null)
            {
                return NotFound();
            }

            _unitOfWork.Customers.Remove(customer);
            _unitOfWork.Complete();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            var customer = Task.Run(() => _unitOfWork.Customers.Find(c => c.Id == id).FirstOrDefault());
            if (customer == null)
            {
                return false;
            }
            return true;
        }
    }
}
