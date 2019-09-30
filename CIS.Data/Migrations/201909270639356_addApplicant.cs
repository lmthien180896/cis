namespace CIS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addApplicant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Fullname = c.String(nullable: false, maxLength: 256),
                        Email = c.String(nullable: false, maxLength: 256),
                        Phone = c.String(nullable: false, maxLength: 256),
                        Resume = c.String(nullable: false, maxLength: 256),
                        JobID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Jobs", t => t.JobID, cascadeDelete: true)
                .Index(t => t.JobID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicants", "JobID", "dbo.Jobs");
            DropIndex("dbo.Applicants", new[] { "JobID" });
            DropTable("dbo.Applicants");
        }
    }
}
