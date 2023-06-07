#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonitoriOn.Models;

namespace MonitoriOn.Data
{
    public class ApplicationDbContext : IdentityDbContext<MonitoriOnUser>
    {
        public DbSet<Models.Monitor> Monitors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<DisplayResolution> DisplayResolutions { get; set; }
        public DbSet<FrameUpdate> FrameUpdates { get; set; }
        public DbSet<MonitoriOnUser> MonitoriOnUsers { get; set; }
        public DbSet<BankDetail> BankDetail { get; set; }
        public DbSet<SupplyDogovor> SupplyDogovors { get; set; }
        public DbSet<SupplyDogovorAccount> SupplyDogovorAccounts { get; set; }
        public DbSet<MonitorsOrder> MonitorsOrders { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
