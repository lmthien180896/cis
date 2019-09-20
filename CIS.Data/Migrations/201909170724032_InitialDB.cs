namespace CIS.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Credentials",
                c => new
                {
                    UserGroupID = c.Int(nullable: false),
                    RoleID = c.String(nullable: false, maxLength: 50),
                    ID = c.Int(nullable: false, identity: true),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupID, cascadeDelete: true)
                .Index(t => t.UserGroupID)
                .Index(t => t.RoleID);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    ID = c.String(nullable: false, maxLength: 50),
                    Name = c.String(nullable: false, maxLength: 256),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.UserGroups",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Errors",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Message = c.String(),
                    StackTrace = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Footers",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Content = c.String(),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.MenuGroups",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Menus",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    URL = c.String(nullable: false, maxLength: 256),
                    DisplayOrder = c.Int(),
                    GroupID = c.Int(nullable: false),
                    Target = c.String(maxLength: 10),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MenuGroups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID);

            CreateTable(
                "dbo.Pages",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                    Content = c.String(),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.PostCategories",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                    Description = c.String(maxLength: 500),
                    ParentID = c.Int(),
                    DisplayOrder = c.Int(),
                    Image = c.String(maxLength: 256),
                    HomeFlag = c.Boolean(),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Posts",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                    CategoryID = c.Int(nullable: false),
                    Image = c.String(maxLength: 256),
                    Description = c.String(maxLength: 500),
                    Content = c.String(),
                    HomeFlag = c.Boolean(),
                    HotFlag = c.Boolean(),
                    ViewCount = c.Int(),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PostCategories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);

            CreateTable(
                "dbo.PostTags",
                c => new
                {
                    PostID = c.Int(nullable: false),
                    TagID = c.String(nullable: false, maxLength: 50, unicode: false),
                })
                .PrimaryKey(t => new { t.PostID, t.TagID })
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);

            CreateTable(
                "dbo.RequestCategories",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 256),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.RequestReports",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    RequestID = c.Int(nullable: false),
                    SupporterID = c.Int(),
                    Note = c.String(maxLength: 500),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Requests", t => t.RequestID, cascadeDelete: true)
                .Index(t => t.RequestID);

            CreateTable(
                "dbo.Requests",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    CategoryID = c.Int(nullable: false),
                    SenderName = c.String(nullable: false, maxLength: 256),
                    Email = c.String(nullable: false, maxLength: 256),
                    Phone = c.String(maxLength: 20),
                    Detail = c.String(nullable: false, maxLength: 500),
                    Place = c.String(nullable: false, maxLength: 256),
                    Files = c.String(maxLength: 256),
                    Code = c.String(maxLength: 6),
                    ClosedDate = c.DateTime(),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RequestCategories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);

            CreateTable(
                "dbo.Slides",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Description = c.String(maxLength: 256),
                    Image = c.String(maxLength: 256),
                    Url = c.String(maxLength: 256),
                    DisplayOrder = c.Int(),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.SupportOnlines",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Department = c.String(maxLength: 50),
                    Address = c.String(maxLength: 256),
                    Skype = c.String(maxLength: 50),
                    Mobile = c.String(maxLength: 50),
                    Email = c.String(maxLength: 50),
                    Yahoo = c.String(maxLength: 50),
                    Facebook = c.String(maxLength: 50),
                    Domain = c.String(maxLength: 256),
                    Status = c.Boolean(nullable: false),
                    DisplayOrder = c.Int(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.SystemConfigs",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 50, unicode: false),
                    ValueString = c.String(maxLength: 50),
                    ValueInt = c.Int(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Username = c.String(nullable: false, maxLength: 50),
                    Password = c.String(nullable: false, maxLength: 256),
                    Fullname = c.String(maxLength: 256),
                    Address = c.String(maxLength: 50),
                    Email = c.String(maxLength: 50),
                    Phone = c.String(maxLength: 50),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.VisitorStatistics",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    VisitedDate = c.DateTime(nullable: false),
                    IPAddress = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.RequestReports", "RequestID", "dbo.Requests");
            DropForeignKey("dbo.Requests", "CategoryID", "dbo.RequestCategories");
            DropForeignKey("dbo.PostTags", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories");
            DropForeignKey("dbo.Menus", "GroupID", "dbo.MenuGroups");
            DropForeignKey("dbo.Credentials", "UserGroupID", "dbo.UserGroups");
            DropForeignKey("dbo.Credentials", "RoleID", "dbo.Roles");
            DropIndex("dbo.Requests", new[] { "CategoryID" });
            DropIndex("dbo.RequestReports", new[] { "RequestID" });
            DropIndex("dbo.PostTags", new[] { "PostID" });
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            DropIndex("dbo.Menus", new[] { "GroupID" });
            DropIndex("dbo.Credentials", new[] { "RoleID" });
            DropIndex("dbo.Credentials", new[] { "UserGroupID" });
            DropTable("dbo.VisitorStatistics");
            DropTable("dbo.Users");
            DropTable("dbo.SystemConfigs");
            DropTable("dbo.SupportOnlines");
            DropTable("dbo.Slides");
            DropTable("dbo.Requests");
            DropTable("dbo.RequestReports");
            DropTable("dbo.RequestCategories");
            DropTable("dbo.PostTags");
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
            DropTable("dbo.Pages");
            DropTable("dbo.Menus");
            DropTable("dbo.MenuGroups");
            DropTable("dbo.Footers");
            DropTable("dbo.Errors");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Roles");
            DropTable("dbo.Credentials");
        }
    }
}