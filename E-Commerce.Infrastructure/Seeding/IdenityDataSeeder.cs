using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Identity.Data;
using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Seeding
{
    internal class IdenityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdenityDataSeeder> _logger;

        public IdenityDataSeeder(StoreIdentityDbContext dbContext, UserManager<ApplicationUser> userManager,
     RoleManager<IdentityRole> roleManager, ILogger<IdenityDataSeeder> logger)
        {
            // تم التعديل هنا: إزالة الـ _ من المتغيرات التي على اليمين
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task SeedAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(ct);
                if (!await _roleManager.Roles.AnyAsync(ct))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!await _userManager.Users.AnyAsync(ct))
                {
                    var user = new ApplicationUser
                    {
                        DisplayName = "MohamedAhmed",
                        Email = "mohamed@gmail.com",
                        UserName = "MohamedAhmed",
                        PhoneNumber = "1234567890",
                    };
                   var createUserResult = await _userManager.CreateAsync(user, "P@ssw0rd");
                    if (createUserResult.Succeeded)
                    { 
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        _logger.LogWarning("Failed to create user: {Errors}", string.Join(", ", createUserResult.Errors.Select( e => e.Description)));
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the identity data.");
                throw;


            }
        }
    }
}
