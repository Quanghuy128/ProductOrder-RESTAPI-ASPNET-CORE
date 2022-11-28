using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductOrder.DataAccess;
using ProductOrder.Models;

namespace ProductOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // inject Account to [controller]
    public class AccountController : Controller
    {
        private readonly ProductOrderDbContext _accountDbContext;

        public AccountController(ProductOrderDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            return Ok(await _accountDbContext.Accounts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
        {
            var foundAccount = await _accountDbContext.Accounts.FindAsync(id);  
            return foundAccount != null ? Ok(foundAccount) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(CreateNewAccountRequest createNewAccountRequest)
        {
            var newAccount = new Account()
            {
                Id = Guid.NewGuid(),
                FullName = createNewAccountRequest.FullName,
                Email = createNewAccountRequest.Email,
                Phone = createNewAccountRequest.Phone,
                CreatedDate = createNewAccountRequest.CreatedDate,
                Status = true,
            };
            await _accountDbContext.Accounts.AddAsync(newAccount);
            await _accountDbContext.SaveChangesAsync();
            return Ok(newAccount);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, UpdateAccountRequest updateAccountRequest)
        {
            var foundAccount = await _accountDbContext.Accounts.FindAsync(id);
            if (foundAccount != null)
            {
                foundAccount.FullName = updateAccountRequest.FullName;
                foundAccount.Email = updateAccountRequest.Email;
                foundAccount.Phone = updateAccountRequest.Phone;
                foundAccount.CreatedDate = updateAccountRequest.CreatedDate;
                foundAccount.Status = updateAccountRequest.Status;


                await _accountDbContext.SaveChangesAsync();
                return Ok(foundAccount);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
        {
            var foundAccount = await _accountDbContext.Accounts.FindAsync(id);
            if(foundAccount != null)
            {
                _accountDbContext.Remove(foundAccount);
                await _accountDbContext.SaveChangesAsync();
                return Ok(foundAccount);
            }
            return NotFound();
        }
    }
}
