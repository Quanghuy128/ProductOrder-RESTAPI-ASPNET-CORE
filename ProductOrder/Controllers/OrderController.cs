using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductOrder.DataAccess;
using ProductOrder.Models;

namespace ProductOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ProductOrderDbContext _context;

        public OrderController(ProductOrderDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders ()
        {
            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateNewOrderRequest createNewOrderRequest)
        {
            var newOrder = new Order()
            {
                Id = Guid.NewGuid(),
                CreatedTime = createNewOrderRequest.CreatedTime,
                AccountId = createNewOrderRequest.AccountId,
            };
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
            return Ok(newOrder);
        }
    }
}
