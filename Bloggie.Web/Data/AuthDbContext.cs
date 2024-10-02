using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles (User, Admin, SuperAdmin)

            var adminRoleId = "EE02ACD6-8A7F-4A65-A2D8-1C0612919310";
            var superAdminRoleId = "5A00D680-5687-4F19-BA97-FDF80019F4A2";
            var userRoleId = "A786C0A8-46FD-4A98-924B-194EA2EF080A";

            var roles = new List<IdentityRole> 
            {
                     new IdentityRole
                     {
                       Name = "Admin",
                       NormalizedName = "Admin",
                       Id = adminRoleId,
                       ConcurrencyStamp = adminRoleId
                     },
                         new IdentityRole
                     {
                       Name = "SuperAdmin",
                       NormalizedName = "SuperAdmin",
                       Id = superAdminRoleId,
                       ConcurrencyStamp = superAdminRoleId
                     },
                     new IdentityRole
                     {
                       Name = "User",
                       NormalizedName = "User",
                       Id = userRoleId,
                       ConcurrencyStamp = userRoleId
                     }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed SuperAdminUser

            var superAdminId = "7CF690F7-5580-4D8A-8D22-3BF043195BAC";
            var superAdminUser = new IdentityUser
            {
                UserName = "SuperAdmin@Bloggie.com",
                Email = "SuperAdmin@Bloggie.com",
                NormalizedEmail = "SuperAdmin@Bloggie.com".ToUpper(),
                NormalizedUserName = "SuperAdmin@Bloggie.com".ToUpper(),
                Id = superAdminId,
                 
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "SuperAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add all the roles to SuperAdminUser

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                     RoleId = adminRoleId,
                     UserId = superAdminId
                },
                 new IdentityUserRole<string>
                {
                     RoleId = superAdminRoleId,
                     UserId = superAdminId
                },
                  new IdentityUserRole<string>
                {
                     RoleId = userRoleId,
                     UserId = superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
