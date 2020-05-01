namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecourse2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "DateCreated", c => c.DateTime());
            AddColumn("dbo.Courses", "UserModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "UserModified");
            DropColumn("dbo.Courses", "DateCreated");
        }
    }
}
