using System;
using System.Threading.Tasks;
using ClientAPI.Data.Shared.DTOs;
using ClientAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ClientAPI.Controllers
{
  
    [ApiController]
    [EnableCors("AllowAll")]
    [Authorize]
    [Route("api/[controller]")]
    public class UserServiceController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceProvider _service;
        public UserServiceController(IServiceProvider service, UserManager<ApplicationUser> userManager)
        {
           _service = service;
           _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        
       
        [HttpGet]
        public async Task<IActionResult> GetUserProfile () {
                      
            var user = await GetCurrentUserAsync();
            
            return Ok(user);
        }
    }
}