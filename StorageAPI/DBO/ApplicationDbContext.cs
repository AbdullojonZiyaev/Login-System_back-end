using Microsoft.EntityFrameworkCore;
using StorageAPI.Models;

namespace StorageAPI.DBO
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public ApplicationDbContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("WebApiDatabase");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        public DbSet<UserInfo> Users { get; set; }
        
    }
}
