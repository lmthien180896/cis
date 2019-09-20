namespace CIS.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddReferenceUrlField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ReferenceUrl", c => c.String(maxLength: 256));
        }

        public override void Down()
        {
            DropColumn("dbo.Posts", "ReferenceUrl");
        }
    }
}