using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestAAI.Models;

namespace TestAAI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
