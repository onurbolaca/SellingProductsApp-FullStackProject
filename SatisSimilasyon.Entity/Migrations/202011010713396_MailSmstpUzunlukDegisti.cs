namespace SatisSimilasyon.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MailSmstpUzunlukDegisti : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MailParams", "SMTP", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MailParams", "SMTP", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
