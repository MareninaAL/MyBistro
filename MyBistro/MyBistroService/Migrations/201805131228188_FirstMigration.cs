namespace MyBistroService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Аcquirente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        АcquirenteFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VitaAssassinas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        АcquirenteId = c.Int(nullable: false),
                        SnackId = c.Int(nullable: false),
                        CuocoId = c.Int(),
                        CuocoFIO = c.Int(),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cuocoes", t => t.CuocoId)
                .ForeignKey("dbo.Snacks", t => t.SnackId, cascadeDelete: true)
                .ForeignKey("dbo.Аcquirente", t => t.АcquirenteId, cascadeDelete: true)
                .Index(t => t.АcquirenteId)
                .Index(t => t.SnackId)
                .Index(t => t.CuocoId);
            
            CreateTable(
                "dbo.Cuocoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CuocoFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Snacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SnackName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConstituentSnacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SnackId = c.Int(nullable: false),
                        ConstituentId = c.Int(nullable: false),
                        ConstituentName = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Constituents", t => t.ConstituentId, cascadeDelete: true)
                .ForeignKey("dbo.Snacks", t => t.SnackId, cascadeDelete: true)
                .Index(t => t.SnackId)
                .Index(t => t.ConstituentId);
            
            CreateTable(
                "dbo.Constituents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConstituentName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefrigeratorConstituents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefrigeratorId = c.Int(nullable: false),
                        ConstituentId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Constituents", t => t.ConstituentId, cascadeDelete: true)
                .ForeignKey("dbo.Refrigerators", t => t.RefrigeratorId, cascadeDelete: true)
                .Index(t => t.RefrigeratorId)
                .Index(t => t.ConstituentId);
            
            CreateTable(
                "dbo.Refrigerators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefrigeratorName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VitaAssassinas", "АcquirenteId", "dbo.Аcquirente");
            DropForeignKey("dbo.VitaAssassinas", "SnackId", "dbo.Snacks");
            DropForeignKey("dbo.ConstituentSnacks", "SnackId", "dbo.Snacks");
            DropForeignKey("dbo.RefrigeratorConstituents", "RefrigeratorId", "dbo.Refrigerators");
            DropForeignKey("dbo.RefrigeratorConstituents", "ConstituentId", "dbo.Constituents");
            DropForeignKey("dbo.ConstituentSnacks", "ConstituentId", "dbo.Constituents");
            DropForeignKey("dbo.VitaAssassinas", "CuocoId", "dbo.Cuocoes");
            DropIndex("dbo.RefrigeratorConstituents", new[] { "ConstituentId" });
            DropIndex("dbo.RefrigeratorConstituents", new[] { "RefrigeratorId" });
            DropIndex("dbo.ConstituentSnacks", new[] { "ConstituentId" });
            DropIndex("dbo.ConstituentSnacks", new[] { "SnackId" });
            DropIndex("dbo.VitaAssassinas", new[] { "CuocoId" });
            DropIndex("dbo.VitaAssassinas", new[] { "SnackId" });
            DropIndex("dbo.VitaAssassinas", new[] { "АcquirenteId" });
            DropTable("dbo.Refrigerators");
            DropTable("dbo.RefrigeratorConstituents");
            DropTable("dbo.Constituents");
            DropTable("dbo.ConstituentSnacks");
            DropTable("dbo.Snacks");
            DropTable("dbo.Cuocoes");
            DropTable("dbo.VitaAssassinas");
            DropTable("dbo.Аcquirente");
        }
    }
}
