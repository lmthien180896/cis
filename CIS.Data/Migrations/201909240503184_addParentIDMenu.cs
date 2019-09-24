namespace CIS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addParentIDMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "ParentId");
        }
    }
}
