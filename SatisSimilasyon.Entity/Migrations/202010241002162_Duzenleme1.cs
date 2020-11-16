namespace SatisSimilasyon.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Duzenleme1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.References", "Definition", c => c.String());
            DropColumn("dbo.References", "Defination");
        }
        
        public override void Down()
        {
            AddColumn("dbo.References", "Defination", c => c.String());
            DropColumn("dbo.References", "Definition");
        }
    }
}
