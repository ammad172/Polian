using Coupon.Authentication.Service.api.Models;
using Coupon.Authentication.Service.api.Models.Model;
using Coupon.Authentication.Service.api.Services.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Coupon.Authentication.Service.api.Services
{
    public class JWTtokengenerator : IJWTtokengenerator
    {

        private readonly JwtOptions _jwtoptions;
        public JWTtokengenerator(IOptions<JwtOptions> jwtoptions)
        {
            _jwtoptions = jwtoptions.Value;
        }

        public string GenerateTokenJWT(ApplicationUser applicationUser, IList<string> roles)
        {
            var tokenholder = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtoptions.Secret);

            var ClaimList = new List<Claim> {

                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
                 new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id),
                  new Claim(JwtRegisteredClaimNames.Name,applicationUser.UserName),
            };

            ClaimList.AddRange(roles.Select(data => new Claim(ClaimTypes.Role, data)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtoptions.Issuer,
                Audience = _jwtoptions.Audience,
                Subject = new ClaimsIdentity(ClaimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenholder.CreateToken(tokenDescriptor);

            return tokenholder.WriteToken(token);

        }

    }
}
