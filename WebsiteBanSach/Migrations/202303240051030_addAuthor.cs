namespace WebsiteBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAuthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "Author", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Product", "Author");
        }
    }
}
