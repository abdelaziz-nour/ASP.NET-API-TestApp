using Microsoft.EntityFrameworkCore;
using TestRestApi.Data.Models;

namespace TestRestApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
