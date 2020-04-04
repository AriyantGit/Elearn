namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRegStu : DbMigration
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
                        TopicListId = c.Int(),
                        DateCreated = c.DateTime(),
                        UserModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.StudentCourseRegistrationId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.TopicLists", t => t.TopicListId)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId)
                .Index(t => t.TopicListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourseRegistrations", "TopicListId", "dbo.TopicLists");
            DropForeignKey("dbo.StudentCourseRegistrations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCourseRegistrations", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentCourseRegistrations", new[] { "TopicListId" });
            DropIndex("dbo.StudentCourseRegistrations", new[] { "CourseId" });
            DropIndex("dbo.StudentCourseRegistrations", new[] { "StudentId" });
            DropTable("dbo.StudentCourseRegistrations");
        }
    }
}
