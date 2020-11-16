namespace SatisSimilasyon.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Degisklik : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MailParams", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MailParams", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
