using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClientAPI.BusinessLayer.Contracts;
using ClientAPI.Data.Shared;
using ClientAPI.Data.Shared.ViewModels;
using ClientAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
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
         private readonly IConfiguration _configuration;
        private readonly IServiceProvider _service;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public AuthController(IServiceProvider service, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        
 
        [HttpPost ("login")]
        public async Task<IActionResult> Login ([FromBody] AuthVM authVM) 
        {      
            
            var result = await _signInManager.PasswordSignInAsync(authVM.Username, authVM.Password, false, false);
            
            if (result.Succeeded)
            {
                var authService = _service.GetService<IAuthService>();

                var auth = await authService.Login(authVM);

                if(auth == null)
                    return Unauthorized();

                var user = _userManager.Users.SingleOrDefault(r => r.Email == authVM.Username);

                var tokenString = new JWTGenerateToken().GenerateJwtToken(auth.Username,auth.UserId , auth,_configuration);
                
                return Ok(new { token = tokenString ,username = authVM.Username });
            }
            
           return Unauthorized();
           
        }

    }
}