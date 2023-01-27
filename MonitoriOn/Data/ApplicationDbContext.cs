#nullable disable

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Models;

namespace MonitoriOn.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Models.Monitor> Monitors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<DisplayResolution> DisplayResolutions { get; set; }
        public DbSet<FrameUpdate> FrameUpdates { get; set; }
        public DbSet<MonitoriOnUser> MonitoriOnUsers { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
