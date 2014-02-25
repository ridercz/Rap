namespace Altairis.Rap.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class Add_Activities : DbMigration {
        public override void Up() {
            CreateTable(
                "dbo.Activities",
                c => new {
                    ActivityId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.ActivityId);
            this.Sql("INSERT INTO Activities (Name) VALUES ('(jiné)')");

            AddColumn("dbo.Bookings", "ActivityId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Bookings", "ActivityId");
            AddForeignKey("dbo.Bookings", "ActivityId", "dbo.Activities", "ActivityId", cascadeDelete: true);
        }

        public override void Down() {
            DropForeignKey("dbo.Bookings", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Bookings", new[] { "ActivityId" });
            DropColumn("dbo.Bookings", "ActivityId");
            DropTable("dbo.Activities");
        }
    }
}
