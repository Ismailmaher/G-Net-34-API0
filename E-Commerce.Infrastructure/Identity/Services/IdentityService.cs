using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOS.Authentication;
using E_Commerce.Infrastructure.Identity.Entities;
using E_Commerce.Infrastructure.Seeding;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Infrastructure.Identity.Services
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
           
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
               
                return Result<bool>.Fail(Error.NotFound("User Not Found"));

           
            var isValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isValid)
                return Result<bool>.Fail(Error.Unauthorized("Invalid email or password"));

           
            return Result<bool>.Ok(true);
        }

        public Task<Result<bool>> CheckPasswordAsync(string email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IdentityUserResult>> CheckUserAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
           var user = new ApplicationUser
           {
               Email = registerDto.Email,
               UserName = registerDto.userName,
               DisplayName = registerDto.DisplayName,
               PhoneNumber = registerDto.Phonenumber
           };
            var result = await _userManager.CreateAsync(user, registerDto.password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }
            return new IdentityUserResult(user.Id, user.Email, user.UserName, user.DisplayName);    
        }

        public async Task<Result<IdentityUserResult>> FindByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
               { 
                    return Result<IdentityUserResult>.Fail(Error.NotFound("User Not Email"));
               }
            else
            {
                return new IdentityUserResult(user.Id, user.Email, user.UserName, user.DisplayName);
            }

        }
    }


}
