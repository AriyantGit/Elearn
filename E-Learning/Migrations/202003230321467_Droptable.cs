namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Droptable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentCourseRegistrations", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentCourseRegistrations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCourseRegistrations", "TopicListId", "dbo.TopicLists");
            DropIndex("dbo.StudentCourseRegistrations", new[] { "StudentId" });
            DropIndex("dbo.StudentCourseRegistrations", new[] { "CourseId" });
            DropIndex("dbo.StudentCourseRegistrations", new[] { "TopicListId" });
            DropTable("dbo.StudentCourseRegistrations");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.StudentCourseRegistrationId);
            
            CreateIndex("dbo.StudentCourseRegistrations", "TopicListId");
            CreateIndex("dbo.StudentCourseRegistrations", "CourseId");
            CreateIndex("dbo.StudentCourseRegistrations", "StudentId");
            AddForeignKey("dbo.StudentCourseRegistrations", "TopicListId", "dbo.TopicLists", "Id");
            AddForeignKey("dbo.StudentCourseRegistrations", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
            AddForeignKey("dbo.StudentCourseRegistrations", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
