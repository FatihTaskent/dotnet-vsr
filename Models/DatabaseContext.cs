using Microsoft.EntityFrameworkCore;

namespace DataAcces
{
    class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Filename=./VSR_TLDR_Database.db");
        }
    }
}