namespace SatisSimilasyon.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablodaDuzenlemeYapildi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MailParams", "Password", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.MailParams", "Paswword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MailParams", "Paswword", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.MailParams", "Password");
        }
    }
}
