using ClientAPI.Models;
using ClientAPI.Models.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClientAPI.Data
{
    public class DataContext :  IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            
             modelBuilder.ApplyConfiguration(new PostsConfig());
             base.OnModelCreating(modelBuilder);

        }
        public DbSet<User> User { get; set; }
        public DbSet<Forums> Forums { get; set; }
        public DbSet<Posts> Posts { get; set; }

    }
}