namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topiclike1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TopicLikes", "TopicId", "dbo.TopicLists");
            DropIndex("dbo.TopicLikes", new[] { "TopicId" });
            DropColumn("dbo.TopicLikes", "TopicId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TopicLikes", "TopicId", c => c.Int(nullable: false));
            CreateIndex("dbo.TopicLikes", "TopicId");
            AddForeignKey("dbo.TopicLikes", "TopicId", "dbo.TopicLists", "Id", cascadeDelete: true);
        }
    }
}
