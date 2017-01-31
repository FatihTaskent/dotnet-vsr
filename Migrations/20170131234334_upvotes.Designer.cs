using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DataAcces;

namespace dotnetvsr.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20170131234334_upvotes")]
    partial class upvotes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("dotnet_core.Models.Account", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("dotnet_core.Models.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountID");

                    b.Property<DateTime>("PostDate");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("dotnet_core.Models.Upvote", b =>
                {
                    b.Property<int>("AccountId");

                    b.Property<int>("MessageId");

                    b.HasKey("AccountId", "MessageId");

                    b.HasIndex("MessageId");

                    b.ToTable("Upvote");
                });

            modelBuilder.Entity("dotnet_core.Models.Message", b =>
                {
                    b.HasOne("dotnet_core.Models.Account", "Account")
                        .WithMany("Messages")
                        .HasForeignKey("AccountID");
                });

            modelBuilder.Entity("dotnet_core.Models.Upvote", b =>
                {
                    b.HasOne("dotnet_core.Models.Account", "Account")
                        .WithMany("Upvotes")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dotnet_core.Models.Message", "Message")
                        .WithMany("Upvotes")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
