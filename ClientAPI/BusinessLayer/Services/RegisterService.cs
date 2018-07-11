using System;
using System.Threading.Tasks;
using AutoMapper;
using ClientAPI.BusinessLayer.Contracts;
using ClientAPI.Data.Contracts;
using ClientAPI.Data.Shared.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using ClientAPI.Models;

namespace BusinessLayer.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IServiceProvider _service;
        public RegisterService(IServiceProvider service)
        {
            _service = service;
        }
        public async Task<bool> Register(UserVM regUser)
        {
            var userRepo = _service.GetService<IUserRepo>();

            var user = Mapper.Map<User>(regUser);       

            var result = await userRepo.Register(user,regUser.Password);

            return result;
        }
        
        public async Task<bool> IsExist(object obj)
        {
            string username = obj.ToString();

            var userRepo = _service.GetService<IUserRepo>();

            var result = await userRepo.UserExist(username);

            return result;
        }
    }
}