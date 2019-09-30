namespace CIS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreatedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "CreatedDate");
        }
    }
}
