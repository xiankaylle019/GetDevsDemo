using System.Threading.Tasks;

namespace ClientAPI.Data.Shared.Contracts
{
    public interface IExist
    {
         Task<bool> IsExist(object obj);
    }
}