using System.ComponentModel.DataAnnotations;

namespace ClientAPI.Data.Shared.ViewModels
{
    public class AuthVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}