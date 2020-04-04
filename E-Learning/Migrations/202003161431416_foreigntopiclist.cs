namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreigntopiclist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TopicLists", "Course_Id", "dbo.Courses");
            DropIndex("dbo.TopicLists", new[] { "Course_Id" });
            RenameColumn(table: "dbo.TopicLists", name: "Course_Id", newName: "CourseId");
            AlterColumn("dbo.TopicLists", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.TopicLists", "CourseId");
            AddForeignKey("dbo.TopicLists", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicLists", "CourseId", "dbo.Courses");
            DropIndex("dbo.TopicLists", new[] { "CourseId" });
            AlterColumn("dbo.TopicLists", "CourseId", c => c.Int());
            RenameColumn(table: "dbo.TopicLists", name: "CourseId", newName: "Course_Id");
            CreateIndex("dbo.TopicLists", "Course_Id");
            AddForeignKey("dbo.TopicLists", "Course_Id", "dbo.Courses", "Id");
        }
    }
}
