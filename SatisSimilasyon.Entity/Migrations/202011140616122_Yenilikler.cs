namespace SatisSimilasyon.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Yenilikler : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Simulations", "ResaleMevcutFiyat", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "ResaleYeniFiyat", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "NonResaleMevcutFiyat", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "NonResaleYeniFiyat", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "ArtisMevcutResale", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "ArtisYeniResale", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "ArtisMevcutNonResale", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "ArtisYeniNonResale", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "MevcutLTA", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "MevcutArtisExportResale", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "YeniArtisExportResale", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "MevcutExportNonResale", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "YeniArtisExportNonResale", c => c.Single(nullable: false));
            AddColumn("dbo.SimulationLines", "CustomerReferenceCode", c => c.String());
            AddColumn("dbo.SimulationLines", "ReferenceCode", c => c.String());
            AlterColumn("dbo.ApprovedSimulations", "ApprovedDefinition", c => c.String());
            DropColumn("dbo.Simulations", "ReleaseMevcutFiyat");
            DropColumn("dbo.Simulations", "ReleaseYeniFiyat");
            DropColumn("dbo.Simulations", "NonReleaseMevcutFiyat");
            DropColumn("dbo.Simulations", "NonReleaseYeniFiyat");
            DropColumn("dbo.Simulations", "ArtisMevcutRelease");
            DropColumn("dbo.Simulations", "ArtisYeniRelease");
            DropColumn("dbo.Simulations", "ArtisMevcutNonRelease");
            DropColumn("dbo.Simulations", "ArtisYeniNonRelease");
            DropColumn("dbo.Simulations", "MavcutLTA");
            DropColumn("dbo.Simulations", "MevcutArtisExportRelase");
            DropColumn("dbo.Simulations", "YeniArtisExportRelase");
            DropColumn("dbo.Simulations", "MevcutArtisExportNonRelase");
            DropColumn("dbo.Simulations", "YeniArtisExportNonRelase");
            DropColumn("dbo.SimulationLines", "CustomerReferanceCode");
            DropColumn("dbo.SimulationLines", "ReferanceCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SimulationLines", "ReferanceCode", c => c.String());
            AddColumn("dbo.SimulationLines", "CustomerReferanceCode", c => c.String());
            AddColumn("dbo.Simulations", "YeniArtisExportNonRelase", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "MevcutArtisExportNonRelase", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "YeniArtisExportRelase", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "MevcutArtisExportRelase", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "MavcutLTA", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "ArtisYeniNonRelease", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "ArtisMevcutNonRelease", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "ArtisYeniRelease", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "ArtisMevcutRelease", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "NonReleaseYeniFiyat", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "NonReleaseMevcutFiyat", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "ReleaseYeniFiyat", c => c.Single(nullable: false));
            AddColumn("dbo.Simulations", "ReleaseMevcutFiyat", c => c.Single(nullable: false));
            AlterColumn("dbo.ApprovedSimulations", "ApprovedDefinition", c => c.Int());
            DropColumn("dbo.SimulationLines", "ReferenceCode");
            DropColumn("dbo.SimulationLines", "CustomerReferenceCode");
            DropColumn("dbo.Simulations", "YeniArtisExportNonResale");
            DropColumn("dbo.Simulations", "MevcutExportNonResale");
            DropColumn("dbo.Simulations", "YeniArtisExportResale");
            DropColumn("dbo.Simulations", "MevcutArtisExportResale");
            DropColumn("dbo.Simulations", "MevcutLTA");
            DropColumn("dbo.Simulations", "ArtisYeniNonResale");
            DropColumn("dbo.Simulations", "ArtisMevcutNonResale");
            DropColumn("dbo.Simulations", "ArtisYeniResale");
            DropColumn("dbo.Simulations", "ArtisMevcutResale");
            DropColumn("dbo.Simulations", "NonResaleYeniFiyat");
            DropColumn("dbo.Simulations", "NonResaleMevcutFiyat");
            DropColumn("dbo.Simulations", "ResaleYeniFiyat");
            DropColumn("dbo.Simulations", "ResaleMevcutFiyat");
        }
    }
}
