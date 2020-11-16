namespace SatisSimilasyon.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Duzenleme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MailParams", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MailParams", "Discriminator");
        }
    }
}
