namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allownullLasttopicid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentCourseRegistrations", "LastTopicId", "dbo.TopicLists");
            DropIndex("dbo.StudentCourseRegistrations", new[] { "LastTopicId" });
            AlterColumn("dbo.StudentCourseRegistrations", "LastTopicId", c => c.Int());
            CreateIndex("dbo.StudentCourseRegistrations", "LastTopicId");
            AddForeignKey("dbo.StudentCourseRegistrations", "LastTopicId", "dbo.TopicLists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourseRegistrations", "LastTopicId", "dbo.TopicLists");
            DropIndex("dbo.StudentCourseRegistrations", new[] { "LastTopicId" });
            AlterColumn("dbo.StudentCourseRegistrations", "LastTopicId", c => c.Int(nullable: false));
            CreateIndex("dbo.StudentCourseRegistrations", "LastTopicId");
            AddForeignKey("dbo.StudentCourseRegistrations", "LastTopicId", "dbo.TopicLists", "Id", cascadeDelete: true);
        }
    }
}
