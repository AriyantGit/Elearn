namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topiclike2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TopicLikes", "TopicId", c => c.Int(nullable: false));
            CreateIndex("dbo.TopicLikes", "TopicId");
            AddForeignKey("dbo.TopicLikes", "TopicId", "dbo.TopicLists", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicLikes", "TopicId", "dbo.TopicLists");
            DropIndex("dbo.TopicLikes", new[] { "TopicId" });
            DropColumn("dbo.TopicLikes", "TopicId");
        }
    }
}
