namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topiclistupdateView1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TopicLists", "Views", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TopicLists", "Views", c => c.Int(nullable: false));
        }
    }
}
