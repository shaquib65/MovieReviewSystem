using MovieReviewSystem.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewSystem.Interfaces
{
    public interface IAuthenticateService
    {
        public JwtSecurityToken Login(LoginModel loginModel);
        public bool Register(RegistrationModel registrationModel);
        public bool RegisterAdmin(RegistrationModel registrationModel);
    }
}
