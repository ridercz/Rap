namespace Altairis.Rap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_Messages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropTable("dbo.Messages");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.MessageId);
            
            CreateIndex("dbo.Messages", "UserId");
            AddForeignKey("dbo.Messages", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
