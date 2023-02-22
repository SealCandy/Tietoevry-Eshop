using API.Data.DTOS;
using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ShopDbContext shopDbContext;
        public CustomersController(ShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }
        /// <summary>
        /// Returns a specific customer depending on the Name and Password
        /// </summary>
        /// <param name="LoginName">The loginName of the user</param>
        /// <param name="Password">The password of the user</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> getCustomer(string LoginName, string Password)
        {
            var result = await shopDbContext.Customers
                .Where(o => o.Name.Equals(LoginName))
                .Select(o => new CustomerDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    City = o.City,
                    Street = o.Street,
                    Surname = o.Surname,
                    Zip = o.Zip,
                })
                .FirstOrDefaultAsync();
            if(result == null)
            {
                return NotFound();
            }
            if(Password == "pogfish420")
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Returns a specific customer depending on the id
        /// </summary>
        /// <param name="id">The Id of the user</param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> getCustomerById(int id)
        {
            var result = await shopDbContext.Customers
                .Where(o => o.Id == id)
                .Select(o => new CustomerDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    City = o.City,
                    Street = o.Street,
                    Surname = o.Surname,
                    Zip = o.Zip,
                })
                .FirstOrDefaultAsync();
                return Ok(result);


        }
        //Try looking up how to do it one query
        /// <summary>
        /// Used for updating a user based on their id
        /// </summary>
        /// <param name="id">Id of the user we want to update</param>
        /// <param name="customer">The edited data we want the user to have</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> updateCustomerById(int id, [FromBody]CustomerDTO customer)
        {
            var Customer = await shopDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if(Customer != null)
            {
                Customer.Name = customer.Name;
                Customer.Surname = customer.Surname;
                Customer.Street = customer.Street;
                Customer.City = customer.City;
                Customer.Zip = customer.Zip;
                await shopDbContext.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return Ok(Customer);
        }
    }
}
