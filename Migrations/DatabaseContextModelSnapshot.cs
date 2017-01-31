using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DataAcces;

namespace dotnetvsr.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("dotnet_core.Models.Message", b =>
                {
                    b.HasOne("dotnet_core.Models.Account", "Account")
                        .WithMany("Messages")
                        .HasForeignKey("AccountID");
                });
        }
    }
}
