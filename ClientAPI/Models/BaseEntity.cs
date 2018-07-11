using System;

namespace ClientAPI.Models
{
    public abstract class BaseEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public Nullable<DateTime> UpdatedOn { get; set; }
    }
}