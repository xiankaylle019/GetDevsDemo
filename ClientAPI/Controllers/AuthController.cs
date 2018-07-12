using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClientAPI.BusinessLayer.Contracts;
using ClientAPI.Data.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ClientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AuthController : ControllerBase
    {
         private readonly IServiceProvider _service;
        private readonly IConfiguration _config;
        public AuthController(IServiceProvider service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }
        
 
        [HttpPost ("login")]
        public async Task<IActionResult> Login ([FromBody] AuthVM authVM) 
        {
            var authService = _service.GetService<IAuthService>();

            var auth = await authService.Login(authVM);

            if(auth == null)
                return Unauthorized();

            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, auth.Id.ToString()),
                    new Claim(ClaimTypes.Name, auth.Username)
                }),
                
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
           
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString ,username = auth.Username });
        }
    }
}