namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createstudentRegistraion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentCourseRegistrations",
                c => new
                    {
                        StudentCourseRegistrationId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        UserModified = c.DateTime(),
                        TopicList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.StudentCourseRegistrationId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.TopicLists", t => t.TopicList_Id)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId)
                .Index(t => t.TopicList_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourseRegistrations", "TopicList_Id", "dbo.TopicLists");
            DropForeignKey("dbo.StudentCourseRegistrations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCourseRegistrations", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentCourseRegistrations", new[] { "TopicList_Id" });
            DropIndex("dbo.StudentCourseRegistrations", new[] { "CourseId" });
            DropIndex("dbo.StudentCourseRegistrations", new[] { "StudentId" });
            DropTable("dbo.StudentCourseRegistrations");
        }
    }
}
