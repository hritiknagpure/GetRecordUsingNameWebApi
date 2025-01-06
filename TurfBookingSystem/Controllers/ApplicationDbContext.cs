using Microsoft.EntityFrameworkCore;
using TurfBookingSystem.Models;

namespace TurfBookingSystem.Controllers
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
    }
}
