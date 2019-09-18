namespace CIS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAuditable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Credentials", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Credentials", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Credentials", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Roles", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Roles", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserGroups", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.UserGroups", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.UserGroups", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Footers", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Footers", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Footers", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pages", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Pages", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Pages", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostCategories", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.PostCategories", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.PostCategories", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Posts", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Posts", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Posts", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequestCategories", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.RequestCategories", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.RequestCategories", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequestReports", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.RequestReports", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.RequestReports", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Requests", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Requests", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Requests", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Credentials", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Credentials", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Roles", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Roles", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.UserGroups", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.UserGroups", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Footers", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Footers", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Pages", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Pages", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.PostCategories", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.PostCategories", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Posts", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Posts", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.RequestCategories", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.RequestCategories", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.RequestReports", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.RequestReports", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Requests", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Requests", "UpdatedBy", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Requests", "CreatedBy", c => c.String());
            AlterColumn("dbo.RequestReports", "UpdatedBy", c => c.String());
            AlterColumn("dbo.RequestReports", "CreatedBy", c => c.String());
            AlterColumn("dbo.RequestCategories", "UpdatedBy", c => c.String());
            AlterColumn("dbo.RequestCategories", "CreatedBy", c => c.String());
            AlterColumn("dbo.Posts", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Posts", "CreatedBy", c => c.String());
            AlterColumn("dbo.PostCategories", "UpdatedBy", c => c.String());
            AlterColumn("dbo.PostCategories", "CreatedBy", c => c.String());
            AlterColumn("dbo.Pages", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Pages", "CreatedBy", c => c.String());
            AlterColumn("dbo.Footers", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Footers", "CreatedBy", c => c.String());
            AlterColumn("dbo.UserGroups", "UpdatedBy", c => c.String());
            AlterColumn("dbo.UserGroups", "CreatedBy", c => c.String());
            AlterColumn("dbo.Roles", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Roles", "CreatedBy", c => c.String());
            AlterColumn("dbo.Credentials", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Credentials", "CreatedBy", c => c.String());
            DropColumn("dbo.Requests", "Status");
            DropColumn("dbo.Requests", "MetaDescription");
            DropColumn("dbo.Requests", "MetaKeyword");
            DropColumn("dbo.RequestReports", "Status");
            DropColumn("dbo.RequestReports", "MetaDescription");
            DropColumn("dbo.RequestReports", "MetaKeyword");
            DropColumn("dbo.RequestCategories", "Status");
            DropColumn("dbo.RequestCategories", "MetaDescription");
            DropColumn("dbo.RequestCategories", "MetaKeyword");
            DropColumn("dbo.Posts", "Status");
            DropColumn("dbo.Posts", "MetaDescription");
            DropColumn("dbo.Posts", "MetaKeyword");
            DropColumn("dbo.PostCategories", "Status");
            DropColumn("dbo.PostCategories", "MetaDescription");
            DropColumn("dbo.PostCategories", "MetaKeyword");
            DropColumn("dbo.Pages", "Status");
            DropColumn("dbo.Pages", "MetaDescription");
            DropColumn("dbo.Pages", "MetaKeyword");
            DropColumn("dbo.Footers", "Status");
            DropColumn("dbo.Footers", "MetaDescription");
            DropColumn("dbo.Footers", "MetaKeyword");
            DropColumn("dbo.UserGroups", "Status");
            DropColumn("dbo.UserGroups", "MetaDescription");
            DropColumn("dbo.UserGroups", "MetaKeyword");
            DropColumn("dbo.Roles", "Status");
            DropColumn("dbo.Roles", "MetaDescription");
            DropColumn("dbo.Roles", "MetaKeyword");
            DropColumn("dbo.Credentials", "Status");
            DropColumn("dbo.Credentials", "MetaDescription");
            DropColumn("dbo.Credentials", "MetaKeyword");
        }
    }
}
