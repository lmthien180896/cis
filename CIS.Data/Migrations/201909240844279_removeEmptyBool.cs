namespace CIS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeEmptyBool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "HomeFlag", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Posts", "HotFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "HotFlag", c => c.Boolean());
            AlterColumn("dbo.Posts", "HomeFlag", c => c.Boolean());
        }
    }
}
