using Microsoft.AspNetCore.Mvc;
using ProductOrder.DataAccess;
using ProductOrder.Models;

namespace ProductOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : Controller
    {
        private readonly ProductOrderDbContext _context;

        public OrderDetailController(ProductOrderDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetOrderDetails([FromRoute]Guid id)
        {
            var orderDetailList = _context.OrderDetails.Where(ordDetail => ordDetail.OrderId.Equals(id.ToString())).OrderBy(ordDetail => ordDetail.Price).ToList();
            if(orderDetailList.Any())
                return Ok(orderDetailList);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderDetail(CreateNewOrderDetailRequest createNewOrderDetailRequest)
        {
            var newOrderDetail = new OrderDetail()
            {
                Id = Guid.NewGuid(),
                Price = createNewOrderDetailRequest.Price,
                Quantity = createNewOrderDetailRequest.Quantity,
                ProductId = createNewOrderDetailRequest.ProductId,
                OrderId = createNewOrderDetailRequest.OrderId,
            };
            await _context.OrderDetails.AddAsync(newOrderDetail);
            await _context.SaveChangesAsync();
            return Ok(newOrderDetail);
        }


    }
}
