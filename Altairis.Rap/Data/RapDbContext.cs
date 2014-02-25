using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Data {
    public class RapDbContext : DbContext {

        static RapDbContext() {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RapDbContext, Migrations.Configuration>());
        }

        public RapDbContext() : base("RapDB") { }

        public DbSet<User> Users { get; set; }

        public DbSet<Resource> Resources { get; set; }
        
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Message> Messages { get; set; }

    }
}