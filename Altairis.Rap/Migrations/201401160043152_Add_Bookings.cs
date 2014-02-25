namespace Altairis.Rap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Bookings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        ResourceId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        DateBegin = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        Notes = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Resources", t => t.ResourceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ResourceId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "ResourceId", "dbo.Resources");
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "ResourceId" });
            DropTable("dbo.Bookings");
        }
    }
}
