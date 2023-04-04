namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNews : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_News", "Title", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_News", "Title", c => c.String());
        }
    }
}
