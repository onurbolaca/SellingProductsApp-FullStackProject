namespace SatisSimilasyon.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ikinciDuzenleme : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApprovedSimilations", newName: "ApprovedSimulations");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ApprovedSimulations", newName: "ApprovedSimilations");
        }
    }
}
