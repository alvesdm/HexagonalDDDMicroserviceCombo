using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Host.Api.IoC;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Host.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class JwtTestController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtTestController(
            IConfiguration configuration,
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            _configuration = configuration;
            _jwtOptions = jwtOptions.Value;
        }

        // GET api/JwtTest
        [HttpGet]
        public async Task<string> Get()
        {
            return await GenerateEncodedToken(10, "mail@mail.com");
        }

        public async Task<string> GenerateEncodedToken(int userId, string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, JwtIssuerOptions.ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),

                new Claim(ClaimTypes.Role, _configuration.GetValue<string>(Constants.Configuration.Service.Name)),
                new Claim(ClaimTypes.Role, "UserTypeA"),
                new Claim(ClaimTypes.Role, "UserTypeB")
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
