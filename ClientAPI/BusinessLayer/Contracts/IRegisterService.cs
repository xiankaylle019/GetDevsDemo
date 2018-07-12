using System.Threading.Tasks;
using ClientAPI.Data.Shared.Contracts;
using ClientAPI.Data.Shared.DTOs;
using ClientAPI.Data.Shared.ViewModels;

namespace ClientAPI.BusinessLayer.Contracts
{
    public interface IRegisterService : IExist
    {
        /// <summary>
        /// Async register user service
        /// </summary>
        /// <param name="regUser"></param>
        /// <returns></returns>
        Task<UserDTO> Register(UserVM regUser);
    }
}