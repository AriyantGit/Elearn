namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createexamchkupdatetutorstudent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExamChecks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        UserModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            AddColumn("dbo.Students", "Disable", c => c.String(defaultValue: "no"));
            AddColumn("dbo.Coupons", "Disable", c => c.String(defaultValue: "no"));
            AddColumn("dbo.TutorDetails", "Disable", c => c.String(defaultValue: "no"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamChecks", "StudentId", "dbo.Students");
            DropForeignKey("dbo.ExamChecks", "CourseId", "dbo.Courses");
            DropIndex("dbo.ExamChecks", new[] { "CourseId" });
            DropIndex("dbo.ExamChecks", new[] { "StudentId" });
            DropColumn("dbo.TutorDetails", "Disable");
            DropColumn("dbo.Coupons", "Disable");
            DropColumn("dbo.Students", "Disable");
            DropTable("dbo.ExamChecks");
        }
    }
}
