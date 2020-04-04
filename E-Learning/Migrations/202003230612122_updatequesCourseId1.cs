namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatequesCourseId1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionSets", "TopicList_Id", "dbo.TopicLists");
            DropIndex("dbo.QuestionSets", new[] { "TopicList_Id" });
            DropColumn("dbo.QuestionSets", "TopicList_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionSets", "TopicList_Id", c => c.Int());
            CreateIndex("dbo.QuestionSets", "TopicList_Id");
            AddForeignKey("dbo.QuestionSets", "TopicList_Id", "dbo.TopicLists", "Id");
        }
    }
}
