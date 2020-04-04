namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topiclist : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TopicLists", "TopicName", c => c.String(nullable: false));
            AlterColumn("dbo.TopicLists", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.TopicLists", "VideoPath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TopicLists", "VideoPath", c => c.String());
            AlterColumn("dbo.TopicLists", "Description", c => c.String());
            AlterColumn("dbo.TopicLists", "TopicName", c => c.String());
        }
    }
}
