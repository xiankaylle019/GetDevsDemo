using System.ComponentModel.DataAnnotations;
using ClientAPI.Data.Shared.Mapping.Contracts;
using ClientAPI.Models;

namespace ClientAPI.Data.Shared.ViewModels
{
    public class UserVM : IMapDestination<User>
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Username { get; set; }        

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage= "You must specify a password with a minimum of 4 characters" )]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name minimum length is 2")]
        public string FirstName { get; set; }
       
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name minimum length is 2")]
        public string LastName { get; set; }

        internal string IdentityId { get; set; }
    }
}