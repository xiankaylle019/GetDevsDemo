using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
    public class RegisterServiceController : ControllerBase
    {
        
         private readonly IConfiguration _configuration;
        private readonly IServiceProvider _service;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RegisterServiceController(IServiceProvider service, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost ("register")]
        public async Task<IActionResult> Register ([FromBody] UserVM regUserVM) {
                      
           
            regUserVM.Username = regUserVM.Username.ToLower();

            var regService = _service.GetService<IRegisterService>();
            
            //Check if is user exist;
            if (await regService.IsExist(regUserVM.Username))
                ModelState.AddModelError("Username", "Username already exist");
            
            //Validate Model State;
            if (!ModelState.IsValid)
                return BadRequest (ModelState); //retruns model state error

            var user = new ApplicationUser
            {
                UserName = regUserVM.Username, 
                Email = regUserVM.Username
            };

            var result = await _userManager.CreateAsync(user, regUserVM.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                regUserVM.IdentityId = user.Id;

                var newUser = await regService.Register(regUserVM);

                var tokenString = new JWTGenerateToken().GenerateJwtToken(regUserVM.Username,newUser.UserId, newUser,_configuration);

                return Ok(new { token = tokenString ,username = regUserVM.Username });

            }

            return BadRequest (result.Errors); 
        }

    }
}