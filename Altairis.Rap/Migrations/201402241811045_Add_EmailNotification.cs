namespace Altairis.Rap.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class Add_EmailNotification : DbMigration {
        public override void Up() {
            AddColumn("dbo.Users", "EmailBookings", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Users", "EmailMessages", c => c.Boolean(nullable: false, defaultValue: true));
        }

        public override void Down() {
            DropColumn("dbo.Users", "EmailMessages");
            DropColumn("dbo.Users", "EmailBookings");
        }
    }
}
