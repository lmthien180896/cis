namespace CIS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProgressField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Progress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "Progress");
        }
    }
}
