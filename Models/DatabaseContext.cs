using dotnet_core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAcces
{
    class DatabaseContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                    
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Filename=./VSR_TLDR_Database.db");
        }
    }
}