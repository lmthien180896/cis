namespace CIS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPhoneField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "Phone", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "Phone");
        }
    }
}
