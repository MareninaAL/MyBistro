namespace MyBistroService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFielMailMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        АcquirenteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Аcquirente", t => t.АcquirenteId)
                .Index(t => t.АcquirenteId);
            
            AddColumn("dbo.Аcquirente", "Mail", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageInfoes", "АcquirenteId", "dbo.Аcquirente");
            DropIndex("dbo.MessageInfoes", new[] { "АcquirenteId" });
            DropColumn("dbo.Аcquirente", "Mail");
            DropTable("dbo.MessageInfoes");
        }
    }
}
