using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductOrder.DataAccess;
using ProductOrder.Models;

namespace ProductOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductOrderDbContext _context;

        public ProductController(ProductOrderDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetProductById([FromRoute]Guid productId)
        {
            var foundProduct = await _context.Products.FindAsync(productId);
            if (foundProduct != null)
            {
                return Ok(foundProduct);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateNewProductRequest createNewProductRequest)
        {
            var newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                ProductName = createNewProductRequest.ProductName,
                Price = createNewProductRequest.Price,
                CreatedDate = createNewProductRequest.CreatedDate,
                Status = true,
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return Ok(newProduct);
        }

    }
}
