using System.Threading.Tasks;
using ClientAPI.Data.Shared.DTOs;
using ClientAPI.Data.Shared.ViewModels;

namespace ClientAPI.BusinessLayer.Contracts
{
    public interface IAuthService
    {
          Task<UserDTO> Login (AuthVM auth);
    }
}