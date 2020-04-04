namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "TutorId", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "TutorDetails_Id", c => c.Int());
            CreateIndex("dbo.Courses", "TutorDetails_Id");
            AddForeignKey("dbo.Courses", "TutorDetails_Id", "dbo.TutorDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "TutorDetails_Id", "dbo.TutorDetails");
            DropIndex("dbo.Courses", new[] { "TutorDetails_Id" });
            DropColumn("dbo.Courses", "TutorDetails_Id");
            DropColumn("dbo.Courses", "TutorId");
        }
    }
}
