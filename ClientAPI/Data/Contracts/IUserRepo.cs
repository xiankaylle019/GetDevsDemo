using System.Threading.Tasks;
using ClientAPI.Data.Core;
using ClientAPI.Models;

namespace ClientAPI.Data.Contracts
{
    public interface IUserRepo : IDataRepository<User>
    {
        Task<User> Login (string username, string password);
        Task<bool> Register(User user, string password);
        Task<bool> UserExist(string username);
    }
}