namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProducts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Product", "SeoDescription", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Product", "SeoDescription", c => c.String(maxLength: 500));
        }
    }
}
