namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topiclike : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TopicLikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        TopicId = c.Int(nullable: false),
                        TopicList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.TopicLists", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("dbo.TopicLists", t => t.TopicList_Id)
                .Index(t => t.StudentId)
                .Index(t => t.TopicId)
                .Index(t => t.TopicList_Id);
            
            AddColumn("dbo.TopicLists", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.TopicLists", "TopicLike_Id", c => c.Int());
            CreateIndex("dbo.TopicLists", "TopicLike_Id");
            AddForeignKey("dbo.TopicLists", "TopicLike_Id", "dbo.TopicLikes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicLikes", "TopicList_Id", "dbo.TopicLists");
            DropForeignKey("dbo.TopicLists", "TopicLike_Id", "dbo.TopicLikes");
            DropForeignKey("dbo.TopicLikes", "TopicId", "dbo.TopicLists");
            DropForeignKey("dbo.TopicLikes", "StudentId", "dbo.Students");
            DropIndex("dbo.TopicLikes", new[] { "TopicList_Id" });
            DropIndex("dbo.TopicLikes", new[] { "TopicId" });
            DropIndex("dbo.TopicLikes", new[] { "StudentId" });
            DropIndex("dbo.TopicLists", new[] { "TopicLike_Id" });
            DropColumn("dbo.TopicLists", "TopicLike_Id");
            DropColumn("dbo.TopicLists", "Views");
            DropTable("dbo.TopicLikes");
        }
    }
}
