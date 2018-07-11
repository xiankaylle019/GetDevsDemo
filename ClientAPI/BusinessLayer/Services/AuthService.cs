using System;
using System.Threading.Tasks;
using AutoMapper;
using ClientAPI.BusinessLayer.Contracts;
using ClientAPI.Data.Contracts;
using ClientAPI.Data.Shared.DTOs;
using ClientAPI.Data.Shared.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ClientAPI.BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IServiceProvider _service;
        public AuthService(IServiceProvider service)
        {
            _service = service;
        }
        public async Task<UserDTO> Login(AuthVM auth)
        {
            var userRepo = _service.GetService<IUserRepo>();
        
            var user = await userRepo.Login(auth.Username, auth.Password);

            var userDTO = Mapper.Map<UserDTO>(user);
           
            return userDTO;
        }
    }
}