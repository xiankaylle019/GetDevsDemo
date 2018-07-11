using System.Threading.Tasks;
using ClientAPI.Data.Shared.DTOs;
using ClientAPI.Data.Shared.ViewModels;

namespace ClientAPI.BusinessLayer.Contracts
{
    public interface IAuthService
    {
        /// <summary>
        /// Async Login for user
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        Task<UserDTO> Login (AuthVM auth);
    }
}