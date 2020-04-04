namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecourse1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Courses", name: "TutorDetails_Id", newName: "TutorDetail_Id");
            RenameIndex(table: "dbo.Courses", name: "IX_TutorDetails_Id", newName: "IX_TutorDetail_Id");
            DropColumn("dbo.Courses", "TutorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "TutorId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Courses", name: "IX_TutorDetail_Id", newName: "IX_TutorDetails_Id");
            RenameColumn(table: "dbo.Courses", name: "TutorDetail_Id", newName: "TutorDetails_Id");
        }
    }
}
