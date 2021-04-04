using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieReviewSystem.Authentication;
using MovieReviewSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieReviewSystem.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<ApplicationIdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticateService(UserManager<ApplicationIdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        public LoginResponseModel Login(LoginModel loginModel)
        {
            var user = userManager.FindByNameAsync(loginModel.Username).Result;
            if (user != null && userManager.CheckPasswordAsync(user, loginModel.Password).Result)
            {
                var userRoles = userManager.GetRolesAsync(user).Result;

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new LoginResponseModel { token = token, userId = user.Id, role = userRoles[0] };
            }
            return null;
        }

        public bool Register(RegistrationModel registrationModel)
        {
            var userExists = userManager.FindByNameAsync(registrationModel.Username).Result;
            if (userExists != null)
                return false;

            ApplicationIdentityUser user = new ApplicationIdentityUser()
            {
                Email = registrationModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registrationModel.Username
            };
            var result = userManager.CreateAsync(user, registrationModel.Password).Result;
            if (!result.Succeeded)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RegisterAdmin(RegistrationModel registrationModel)
        {
            var userExists = userManager.FindByNameAsync(registrationModel.Username).Result;
            if (userExists != null)
                return false;

            ApplicationIdentityUser user = new ApplicationIdentityUser()
            {
                Email = registrationModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registrationModel.Username
            };
            var result = userManager.CreateAsync(user, registrationModel.Password).Result;
            if (!result.Succeeded)
                return false;
            if (!roleManager.RoleExistsAsync(UserRoles.Admin).Result)
            {
               var role =  roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!roleManager.RoleExistsAsync(UserRoles.User).Result)
            {
               var role =  roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if ( roleManager.RoleExistsAsync(UserRoles.Admin).Result)
            {
                 userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return true;

        }
    }
}
