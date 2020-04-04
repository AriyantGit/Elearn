namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtutorid2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "TutorId", "dbo.TutorDetails");
            DropIndex("dbo.Courses", new[] { "TutorId" });
            RenameColumn(table: "dbo.Courses", name: "TutorId", newName: "TutorDetail_Id");
            AlterColumn("dbo.Courses", "TutorDetail_Id", c => c.Int());
            CreateIndex("dbo.Courses", "TutorDetail_Id");
            AddForeignKey("dbo.Courses", "TutorDetail_Id", "dbo.TutorDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "TutorDetail_Id", "dbo.TutorDetails");
            DropIndex("dbo.Courses", new[] { "TutorDetail_Id" });
            AlterColumn("dbo.Courses", "TutorDetail_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Courses", name: "TutorDetail_Id", newName: "TutorId");
            CreateIndex("dbo.Courses", "TutorId");
            AddForeignKey("dbo.Courses", "TutorId", "dbo.TutorDetails", "Id", cascadeDelete: true);
        }
    }
}
