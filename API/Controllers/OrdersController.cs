using API.Data.DTOS;
using API.Data;
using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ShopDbContext shopDbContext;
        public OrdersController(ShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }
        /// <summary>
        /// Gets orders from a certain date period
        /// </summary>
        /// <param name="FromDateTime">Where the time period starts</param>
        /// <param name="ToDateTime">Where the time period ends</param>
        /// <param name="Page">Which page do we want to be on from selection</param>
        /// <param name="PageSize">Number of items that each page will contain</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> getOrders(DateTime FromDateTime,DateTime ToDateTime, int Page, int PageSize)
        {
            if (Page <= 0)
            {
                Page = 1;
            }
                var result = await shopDbContext.Orders
                .Where(o => o.DateTime >= FromDateTime && o.DateTime <= ToDateTime)
                .Select(o => new OrderCustomerDTO
                {
                    Id = o.Id,
                    CustomerId = o.Customer.Id,
                    DateTime = o.DateTime,
                    OrderState = o.OrderState,
                    Number = o.Number,
                })
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
            if (result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
        /// <summary>
        /// Returns orders for a certain user
        /// </summary>
        /// <param name="id">Used for getting orders for a user</param>
        /// <returns></returns>
        [HttpGet ("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> getOrder(int id)
        {
            if (shopDbContext.Customers.Where(o => o.Id == id).Count() == 0)
            {
                return NotFound();
            }
            return Ok(await shopDbContext.Orders
                .Where(o => o.Customer.Id == id)
                .Include(o => o.OrderLines)
                .ThenInclude(o => o.ProductVariant)
                .Select(o => new OrderDTO
                {
                    DateTime = o.DateTime,
                    Number = o.Number,
                    OrderState = o.OrderState,
                    OrderLinesDTO = Convertor.ConverOrderLineToDtoList(o.OrderLines, shopDbContext)
                })
                .ToListAsync());
        }
        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="CurrentCustomerId">Who wants the order we are creating</param>
        /// /// <param name="orderDTO">Which cutomer is ordering</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> createOrder(int CurrentCustomerId, [FromBody] NewOrderDTO orderDTO)//Customer id int body
        {//Http request
            if (shopDbContext.Customers.Where(o => o.Id == CurrentCustomerId).Count() == 0)
            {
                return BadRequest();
            }
            shopDbContext.Orders.Add(new Order
            {
                DateTime = DateTime.Now,
                Customer = shopDbContext.Customers.Where(o => o.Id.Equals(CurrentCustomerId)).FirstOrDefault(),
                OrderState = CodeFirst2._0.Enum.State.NEW,
                Number = (DateTime.Now.Year % 100).ToString() + CurrentCustomerId,
                OrderLines = Convertor.ConvertOrderLineDTOToList(orderDTO.OrderLinesDTO, shopDbContext)
            }); 
            await shopDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
