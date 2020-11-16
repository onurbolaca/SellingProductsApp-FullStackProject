namespace SatisSimilasyon.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstCheckIn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApprovedSimilations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        ObjectStatus = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ProductGroup_Id = c.Int(),
                        Simulation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroup_Id)
                .ForeignKey("dbo.Simulations", t => t.Simulation_Id)
                .Index(t => t.ProductGroup_Id)
                .Index(t => t.Simulation_Id);
            
            CreateTable(
                "dbo.MailParams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EMail = c.String(nullable: false, maxLength: 50),
                        Paswword = c.String(nullable: false, maxLength: 15),
                        SMTP = c.String(nullable: false, maxLength: 15),
                        Port = c.Int(nullable: false),
                        SSL = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        ObjectStatus = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        NotiId = c.Int(nullable: false),
                        ContAction = c.String(),
                        Description = c.String(),
                        NotificationStatus = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        ObjectStatus = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Surname = c.String(nullable: false, maxLength: 20),
                        EMail = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 15),
                        Department = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        ObjectStatus = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReferenceId = c.Int(nullable: false),
                        LastPriceLog = c.Single(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        ObjectStatus = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.References", t => t.ReferenceId, cascadeDelete: true)
                .Index(t => t.ReferenceId);
            
            CreateTable(
                "dbo.References",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 15),
                        Name = c.String(nullable: false, maxLength: 15),
                        LastPrice = c.Single(nullable: false),
                        ReferenceGroup = c.Int(nullable: false),
                        LocalOrExport = c.Int(nullable: false),
                        ProductGroupId = c.Int(nullable: false),
                        CustomerReferenceCode = c.String(),
                        SalesQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Defination = c.String(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        ObjectStatus = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.ProductGroupId);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Oran1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Oran2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        ObjectStatus = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Simulations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SimulationName = c.String(),
                        ReleaseMevcutFiyat = c.Single(nullable: false),
                        ReleaseYeniFiyat = c.Single(nullable: false),
                        NonReleaseMevcutFiyat = c.Single(nullable: false),
                        NonReleaseYeniFiyat = c.Single(nullable: false),
                        MevcutEnflasyon = c.Single(nullable: false),
                        YeniEnflasyon = c.Single(nullable: false),
                        ArtisMevcutRelease = c.Single(nullable: false),
                        ArtisYeniRelease = c.Single(nullable: false),
                        ArtisMevcutNonRelease = c.Single(nullable: false),
                        ArtisYeniNonRelease = c.Single(nullable: false),
                        MavcutLTA = c.Single(nullable: false),
                        YeniLTA = c.Single(nullable: false),
                        MevcutArtisExportRelase = c.Single(nullable: false),
                        YeniArtisExportRelase = c.Single(nullable: false),
                        MevcutArtisExportNonRelase = c.Single(nullable: false),
                        YeniArtisExportNonRelase = c.Single(nullable: false),
                        SimulationType = c.Int(),
                        ProductGroupId = c.Int(nullable: false),
                        LocalOrExport = c.Int(nullable: false),
                        SimulationStatus = c.Int(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        ObjectStatus = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.ProductGroupId);
            
            CreateTable(
                "dbo.SimulationLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SimulationId = c.Int(nullable: false),
                        CustomerReferanceCode = c.String(),
                        ReferanceCode = c.String(),
                        ProductType = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        NewPrice = c.Single(nullable: false),
                        SalesQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesPriceDifference = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TurnoverDifference = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastModifiedBy = c.String(),
                        ObjectStatus = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Simulations", t => t.SimulationId, cascadeDelete: true)
                .Index(t => t.SimulationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SimulationLines", "SimulationId", "dbo.Simulations");
            DropForeignKey("dbo.Simulations", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ApprovedSimilations", "Simulation_Id", "dbo.Simulations");
            DropForeignKey("dbo.PriceLogs", "ReferenceId", "dbo.References");
            DropForeignKey("dbo.References", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ApprovedSimilations", "ProductGroup_Id", "dbo.ProductGroups");
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropIndex("dbo.SimulationLines", new[] { "SimulationId" });
            DropIndex("dbo.Simulations", new[] { "ProductGroupId" });
            DropIndex("dbo.ApprovedSimilations", new[] { "Simulation_Id" });
            DropIndex("dbo.PriceLogs", new[] { "ReferenceId" });
            DropIndex("dbo.References", new[] { "ProductGroupId" });
            DropIndex("dbo.ApprovedSimilations", new[] { "ProductGroup_Id" });
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropTable("dbo.SimulationLines");
            DropTable("dbo.Simulations");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.References");
            DropTable("dbo.PriceLogs");
            DropTable("dbo.Users");
            DropTable("dbo.Notifications");
            DropTable("dbo.MailParams");
            DropTable("dbo.ApprovedSimilations");
        }
    }
}
