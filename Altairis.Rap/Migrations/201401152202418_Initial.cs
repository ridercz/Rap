namespace Altairis.Rap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 50),
                        Body = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.Binary(nullable: false, maxLength: 64),
                        PasswordSalt = c.Binary(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 200),
                        Comment = c.String(maxLength: 200),
                        IsApproved = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastLogin = c.DateTime(),
                        DateLastActivity = c.DateTime(),
                        DateLastPasswordChange = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}
