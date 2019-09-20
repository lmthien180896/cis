namespace CIS.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addTagsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                {
                    ID = c.String(nullable: false, maxLength: 50, unicode: false),
                    Name = c.String(nullable: false, maxLength: 50),
                    Type = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("dbo.Tags");
        }
    }
}