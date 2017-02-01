using dotnet_core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAcces
{
    class DatabaseContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                // Upvotes
                modelBuilder.Entity<Upvote>()
                    .HasKey(t => new {t.AccountId, t.MessageId});

                // set foreign key for acount.upvotes
                modelBuilder.Entity<Upvote>()
                    .HasOne(am => am.Account)
                    .WithMany(a => a.Upvotes)
                    .HasForeignKey(am => am.AccountId);
                
                // set foreign key for messages.upvotes
                modelBuilder.Entity<Upvote>()
                    .HasOne(am => am.Message)
                    .WithMany(a => a.Upvotes)
                    .HasForeignKey(am => am.MessageId);

                // Favorites
                modelBuilder.Entity<Favorite>()
                    .HasKey(t => new {t.AccountId, t.MessageId});

                // set foreign key for acount.upvotes
                modelBuilder.Entity<Favorite>()
                    .HasOne(am => am.Account)
                    .WithMany(a => a.Favorites)
                    .HasForeignKey(am => am.AccountId);
                
                // set foreign key for messages.upvotes
                modelBuilder.Entity<Favorite>()
                    .HasOne(am => am.Message)
                    .WithMany(a => a.Favorites)
                    .HasForeignKey(am => am.MessageId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Filename=./VSR_TLDR_Database.db");
        }
    }
}