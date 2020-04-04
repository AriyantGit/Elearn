namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatequesCourseId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionSets", "TopicId", "dbo.TopicLists");
            DropIndex("dbo.QuestionSets", new[] { "TopicId" });
            RenameColumn(table: "dbo.QuestionSets", name: "TopicId", newName: "TopicList_Id");
            AddColumn("dbo.QuestionSets", "CourseId", c => c.Int(nullable: false));
            AlterColumn("dbo.QuestionSets", "TopicList_Id", c => c.Int());
            CreateIndex("dbo.QuestionSets", "CourseId");
            CreateIndex("dbo.QuestionSets", "TopicList_Id");
            AddForeignKey("dbo.QuestionSets", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.QuestionSets", "TopicList_Id", "dbo.TopicLists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionSets", "TopicList_Id", "dbo.TopicLists");
            DropForeignKey("dbo.QuestionSets", "CourseId", "dbo.Courses");
            DropIndex("dbo.QuestionSets", new[] { "TopicList_Id" });
            DropIndex("dbo.QuestionSets", new[] { "CourseId" });
            AlterColumn("dbo.QuestionSets", "TopicList_Id", c => c.Int(nullable: false));
            DropColumn("dbo.QuestionSets", "CourseId");
            RenameColumn(table: "dbo.QuestionSets", name: "TopicList_Id", newName: "TopicId");
            CreateIndex("dbo.QuestionSets", "TopicId");
            AddForeignKey("dbo.QuestionSets", "TopicId", "dbo.TopicLists", "Id", cascadeDelete: true);
        }
    }
}
