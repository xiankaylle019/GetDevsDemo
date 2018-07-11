using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClientAPI.Models
{
    public class Forums : BaseEntity
    {
        public Forums()
        {
            Posts = new Collection<Posts>();
        }
        public int ForumsId { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }

    }
}