using Microsoft.EntityFrameworkCore;
using MinimizeApi.Models.Entities;

namespace MinimizeApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
