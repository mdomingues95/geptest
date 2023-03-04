using geptest.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace geptest.Context
{
    public class SqlLiteContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public SqlLiteContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Product> Product { get; set; }
    }
}