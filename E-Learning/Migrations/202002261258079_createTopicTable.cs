namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTopicTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TopicLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopicName = c.String(),
                        Description = c.String(),
                        VideoPath = c.String(),
                        PdfContent = c.Binary(),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicLists", "Course_Id", "dbo.Courses");
            DropIndex("dbo.TopicLists", new[] { "Course_Id" });
            DropTable("dbo.TopicLists");
        }
    }
}
