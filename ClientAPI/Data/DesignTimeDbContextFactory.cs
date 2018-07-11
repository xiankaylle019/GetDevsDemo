using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ClientAPI.Data
{
    public class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
       where TContext : DbContext
    {

        public TContext CreateDbContext(string[] args)

        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var builder = new DbContextOptionsBuilder<TContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            var dbContext = (TContext)Activator.CreateInstance(typeof(TContext),builder.Options);

            return dbContext;
        }
    }
}