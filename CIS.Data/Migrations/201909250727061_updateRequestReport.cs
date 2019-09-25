namespace CIS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRequestReport : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RequestReports", "SupporterID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RequestReports", "SupporterID", c => c.Int());
        }
    }
}
