using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HRLeaveManagement.Identity
{
    public class SeedIdentity
    {
        public static async Task SeedIdentityData(LeaveManagementIdentityDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                     new IdentityRole
                    {
                        Id = "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                        Name = "Employee",
                        NormalizedName = "EMPLOYEE"
                    },
                    new IdentityRole
                    {
                        Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR"
                    }
                };

                await context.Roles.AddRangeAsync(roles);
            }

            if (!context.UserRoles.Any())
            {
                var userRoles = new List<IdentityUserRole<string>>
                {
                    new IdentityUserRole<string>
                    {
                        RoleId = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                        UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                    },
                    new IdentityUserRole<string>
                    {
                        RoleId = "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                        UserId = "9e224968-33e4-4652-b7b7-8574d048cdb9"
                    }
                };

                await context.UserRoles.AddRangeAsync(userRoles);
            }

            if (!context.Users.Any())
            {
                var hasher = new PasswordHasher<ApplicationUser>();

                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                         Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                         Email = "admin@localhost.com",
                         NormalizedEmail = "ADMIN@LOCALHOST.COM",
                         FirstName = "System",
                         LastName = "Admin",
                         UserName = "admin@localhost.com",
                         NormalizedUserName = "ADMIN@LOCALHOST.COM",
                         PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                         EmailConfirmed = true
                    },
                    new ApplicationUser
                    {
                         Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                         Email = "user@localhost.com",
                         NormalizedEmail = "USER@LOCALHOST.COM",
                         FirstName = "System",
                         LastName = "User",
                         UserName = "user@localhost.com",
                         NormalizedUserName = "USER@LOCALHOST.COM",
                         PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                         EmailConfirmed = true
                    }
                };

                await context.Users.AddRangeAsync(users);
            }

            await context.SaveChangesAsync();
        }
    }
}
