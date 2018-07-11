namespace ClientAPI.Models
{
    public class Posts : BaseEntity
    {
        public string Post { get; set; }        
        public int UserId { get; set; }
        public int ForumsId { get; set; }
    }
}