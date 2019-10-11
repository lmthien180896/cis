namespace CIS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addInternalDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InternalDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        FileUrl = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InternalDetails");
        }
    }
}
