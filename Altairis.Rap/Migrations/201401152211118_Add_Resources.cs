namespace Altairis.Rap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Resources : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ResourceId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.ResourceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Resources");
        }
    }
}
