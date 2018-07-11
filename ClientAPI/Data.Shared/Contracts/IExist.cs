using System.Threading.Tasks;

namespace ClientAPI.Data.Shared.Contracts
{
    public interface IExist
    {
        /// <summary>
        /// Validate object if is already exist
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
         Task<bool> IsExist(object obj);
    }
}