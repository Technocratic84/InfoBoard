using Microsoft.EntityFrameworkCore;
using InfoBoard.Data.Models;
using System.Runtime.CompilerServices;

namespace InfoBoard.Data
{
    public class InfoDBcontext : DbContext
    {
        string _connectionString = "Data Source=PF3VL379\\SQLEXPRESS;Initial Catalog=Snowflake;Integrated Security=True;Connect Timeout=40;Encrypt=False;";

        public DbSet<InfoBoardData> InfoBoardData => Set<InfoBoardData>();
        public DbSet<InfoBoardDevices> InfoBoardDevices => Set<InfoBoardDevices>();
        public DbSet<InfoBoardTypes> InfoBoardTypes => Set<InfoBoardTypes>();
        public DbSet<InfoBoardSettings> InfoBoardSettings => Set<InfoBoardSettings>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        //public InfoDBcontext(DbContextOptions<InfoDBcontext> options) : base(options)
        //public InfoDBcontext()
        //{
        //  this.ChangeTracker.LazyLoadingEnabled = false;
        //}
    }
}
