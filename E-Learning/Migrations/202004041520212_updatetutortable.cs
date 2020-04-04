namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetutortable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TutorDetails", "DateCreated", c => c.DateTime());
            AddColumn("dbo.TutorDetails", "UserModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TutorDetails", "UserModified");
            DropColumn("dbo.TutorDetails", "DateCreated");
        }
    }
}
