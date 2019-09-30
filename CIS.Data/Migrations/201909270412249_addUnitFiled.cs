namespace CIS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUnitFiled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "Unit", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "Unit");
        }
    }
}
