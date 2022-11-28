using Microsoft.EntityFrameworkCore;
using ProductOrder.Models;

namespace ProductOrder.DataAccess
{
    public class ProductOrderDbContext : DbContext
    {
        public ProductOrderDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
