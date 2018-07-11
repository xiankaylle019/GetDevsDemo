using ClientAPI.Models;
using ClientAPI.Models.Config;
using Microsoft.EntityFrameworkCore;

namespace ClientAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            
             modelBuilder.ApplyConfiguration(new PostsConfig());
             base.OnModelCreating(modelBuilder);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Forums> Forums { get; set; }
        public DbSet<Posts> Posts { get; set; }

    }
}