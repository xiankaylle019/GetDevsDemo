using System;
using System.Threading.Tasks;
using ClientAPI.BusinessLayer.Contracts;
using ClientAPI.Data.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ClientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class RegisterServiceController : ControllerBase
    {
        private readonly IServiceProvider _service;
        public RegisterServiceController(IServiceProvider service)
        {
           _service = service;
        }

        [AllowAnonymous]
        [HttpPost ("register")]
        public async Task<IActionResult> Register ([FromBody] UserVM regUserVM) {
                      
            var regService = _service.GetService<IRegisterService>();
             
            regUserVM.Username = regUserVM.Username.ToLower();

            //Check if is user exist;
            if (await regService.IsExist(regUserVM.Username))
                ModelState.AddModelError ("Username", "Username already exist");
            
            //validate request;
            if (!ModelState.IsValid)
                return BadRequest (ModelState); //retruns model state error
    
            bool isCreated = await regService.Register(regUserVM);

            return Ok(isCreated);
        }
    }
}