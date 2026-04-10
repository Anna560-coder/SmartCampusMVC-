using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartCampusMVC.Models;

namespace SmartCampusMVC.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected AppDbContext()
        {
        }

        public DbSet<Issues> Issues { get; set; }
    }
}
