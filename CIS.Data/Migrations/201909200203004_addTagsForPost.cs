namespace CIS.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addTagsForPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Tags", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Posts", "Tags");
        }
    }
}