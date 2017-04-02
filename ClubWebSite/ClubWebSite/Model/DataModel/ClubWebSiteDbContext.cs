using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubWebSite.Model.DataModel
{

    public class ClubWebSiteDbContext : DbContext
    {
        public ClubWebSiteDbContext(DbContextOptions<ClubWebSiteDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Active> Actives { get; set; }

        public DbSet<Enroll> Enrolls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(m => m.ID);
            builder.Entity<Active>().HasKey(m => m.ID);
            builder.Entity<Enroll>().HasKey(m => m.ID);
            base.OnModelCreating(builder);
        }
    }
}
