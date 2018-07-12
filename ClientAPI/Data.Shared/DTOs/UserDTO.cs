using ClientAPI.Data.Shared.Mapping.Contracts;
using ClientAPI.Models;

namespace ClientAPI.Data.Shared.DTOs
{
    public class UserDTO :  IMapSource<User>
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{ this.FirstName } { this.LastName }";}
        
    }
}