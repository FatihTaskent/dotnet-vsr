using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DataAccess;

namespace dotnetvsr.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20170201152008_setup")]
    partial class setup
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

            modelBuilder.Entity("dotnet_core.Models.Favorite", b =>
                {
                    b.Property<int>("AccountId");

                    b.Property<int>("MessageId");

                    b.HasKey("AccountId", "MessageId");

                    b.HasIndex("MessageId");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("dotnet_core.Models.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountID");

                    b.Property<string>("ImageUrl");

                    b.Property<int?>("ParentMessageID");

                    b.Property<DateTime>("PostDate");

                    b.Property<int?>("TagID");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.HasIndex("ParentMessageID");

                    b.HasIndex("TagID");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("dotnet_core.Models.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("dotnet_core.Models.Upvote", b =>
                {
                    b.Property<int>("AccountId");

                    b.Property<int>("MessageId");

                    b.HasKey("AccountId", "MessageId");

                    b.HasIndex("MessageId");

                    b.ToTable("Upvote");
                });

            modelBuilder.Entity("dotnet_core.Models.Favorite", b =>
                {
                    b.HasOne("dotnet_core.Models.Account", "Account")
                        .WithMany("Favorites")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dotnet_core.Models.Message", "Message")
                        .WithMany("Favorites")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnet_core.Models.Message", b =>
                {
                    b.HasOne("dotnet_core.Models.Account", "Account")
                        .WithMany("Messages")
                        .HasForeignKey("AccountID");

                    b.HasOne("dotnet_core.Models.Message", "ParentMessage")
                        .WithMany()
                        .HasForeignKey("ParentMessageID");

                    b.HasOne("dotnet_core.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagID");
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
