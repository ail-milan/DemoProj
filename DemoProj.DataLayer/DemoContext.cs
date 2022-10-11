using DemoProj.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DemoProj.DataLayer
{
    public class DemoContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private string _connectionString = string.Empty;
        public string ConnectionString { get { return _connectionString; } }

        public DemoContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("DemoProj_DB");
            _connectionString = connectionString;
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("DemoProj.API"));
        }

        public virtual DbSet<ORGANIZATIONS> ORGANIZATIONS { get; set; } = null!;
    }
}
