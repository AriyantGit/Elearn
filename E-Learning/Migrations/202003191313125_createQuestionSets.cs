namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createQuestionSets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionSets",
                c => new
                    {
                        QuestionSetId = c.Int(nullable: false, identity: true),
                        QuestionDescription = c.String(nullable: false),
                        Option1 = c.String(nullable: false),
                        Option2 = c.String(nullable: false),
                        Option3 = c.String(nullable: false),
                        Option4 = c.String(nullable: false),
                        CorrectAnswer = c.Int(nullable: false),
                        TopicId = c.Int(nullable: false),
                        Mark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionSetId)
                .ForeignKey("dbo.TopicLists", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.TopicId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionSets", "TopicId", "dbo.TopicLists");
            DropIndex("dbo.QuestionSets", new[] { "TopicId" });
            DropTable("dbo.QuestionSets");
        }
    }
}
