namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtutorid3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "TutorDetail_Id", "dbo.TutorDetails");
            DropIndex("dbo.Courses", new[] { "TutorDetail_Id" });
            RenameColumn(table: "dbo.Courses", name: "TutorDetail_Id", newName: "TutorId");
            AlterColumn("dbo.Courses", "TutorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "TutorId");
            AddForeignKey("dbo.Courses", "TutorId", "dbo.TutorDetails", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "TutorId", "dbo.TutorDetails");
            DropIndex("dbo.Courses", new[] { "TutorId" });
            AlterColumn("dbo.Courses", "TutorId", c => c.Int());
            RenameColumn(table: "dbo.Courses", name: "TutorId", newName: "TutorDetail_Id");
            CreateIndex("dbo.Courses", "TutorDetail_Id");
            AddForeignKey("dbo.Courses", "TutorDetail_Id", "dbo.TutorDetails", "Id");
        }
    }
}
