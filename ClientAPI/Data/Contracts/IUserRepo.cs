using System.Threading.Tasks;
using ClientAPI.Data.Core;
using ClientAPI.Models;

namespace ClientAPI.Data.Contracts
{
    public interface IUserRepo : IDataRepository<User>
    {
        /// <summary>
        /// Async user login method
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Login (string username, string password);
        /// <summary>
        /// Async user registration method
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> Register(User user, string password);
        /// <summary>
        /// Validates if user is already exist.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>

        Task<bool> UserExist(string username);
    }
}