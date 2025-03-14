using System.Reflection.Emit;
using JWTMicroservice.Models;
using JWTMicroservice.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWTMicroservice.Authentication
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define relationship between ApplicationUser and UserDetails
            //builder.Entity<Invoice>()
            //    .HasOne(i => i.Dealer)
            //    .WithMany(u => u.InvoiceDealers)
            //    .HasForeignKey(i => i.DealerId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<Invoice>()
            //    .HasOne(i => i.Farmer)
            //    .WithMany(u => u.InvoiceFarmers)
            //    .HasForeignKey(i => i.FarmerId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<Invoice>().Ignore(i => i.Dealer);

        }
    }
}
