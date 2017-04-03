using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ClubWebSite.Model.DataModel;

namespace ClubWebSite.Migrations
{
    [DbContext(typeof(ClubWebSiteDbContext))]
    [Migration("20170403122340_DbMigration")]
    partial class DbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("ClubWebSite.Model.DataModel.Active", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("BeginTime");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime>("EndTime");

                    b.Property<bool>("IsEnroll");

                    b.Property<string>("Logo");

                    b.Property<string>("Name");

                    b.Property<int>("PeopleNumber");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Actives");
                });

            modelBuilder.Entity("ClubWebSite.Model.DataModel.Enroll", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActiveID");

                    b.Property<string>("Contact");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("ActiveID");

                    b.ToTable("Enrolls");
                });

            modelBuilder.Entity("ClubWebSite.Model.DataModel.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClubWebSite.Model.DataModel.Active", b =>
                {
                    b.HasOne("ClubWebSite.Model.DataModel.User", "CreateUser")
                        .WithMany("Actives")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ClubWebSite.Model.DataModel.Enroll", b =>
                {
                    b.HasOne("ClubWebSite.Model.DataModel.Active", "Active")
                        .WithMany("Enrolls")
                        .HasForeignKey("ActiveID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
