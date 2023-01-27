using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Models;

namespace MonitoriOn.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Models.Monitor> Monitors { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<DisplayResolution> DisplayResolutions { get; set; } = null!;
        public DbSet<FrameUpdate> FrameUpdates { get; set; } = null!;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
