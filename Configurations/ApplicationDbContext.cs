using asp_net_ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_net_ecommerce_api.Configurations;

public class ApplicationDbContext : DbContext{
public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Shirts> Shirts { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<User> Users { get; set; }
}