using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClientAPI.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Forums = new Collection<Forums>();

            Posts = new Collection<Posts>();
        }
        public int UserId { get; set; }
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Forums> Forums { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
    }
}