using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotesWithAutotagging.Contracts.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesWithAutotagging.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/token/")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> logger;
        private IConfiguration configuration;

        public TokenController(ILogger<TokenController> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult GenerateToken(User user)
        {
            if (user.Name == "dev")
            {
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("id", user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Name, user.Name)
                     }),
                    Expires = DateTime.UtcNow.AddDays(14),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(stringToken);
            }
            return Unauthorized();
        }
    }
}