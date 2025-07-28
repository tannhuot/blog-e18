using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace blog_e18.Models
{
    public class AppDBContext(IConfiguration configuration) : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration.GetConnectionString("conn");
            //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Category> Categories { get; set; }
    }
}