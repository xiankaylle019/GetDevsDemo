using System.Threading.Tasks;
using ClientAPI.Data.Shared.Contracts;
using ClientAPI.Data.Shared.ViewModels;

namespace ClientAPI.BusinessLayer.Contracts
{
    public interface IRegisterService : IExist
    {
         Task<bool> Register(UserVM regUser);
    }
}