using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAPI.Data
{
    public class NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options)
        : IdentityDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seeding roles into database
            var roles = new List<IdentityRole>
            {
                new()
                {
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new()
                {
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
