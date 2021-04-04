using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewSystem.Authentication
{
    public class LoginResponseModel
    {
        public JwtSecurityToken token { get; set; }
        public string userId { get; set; }
        public string role { get; set; }
    }
}
