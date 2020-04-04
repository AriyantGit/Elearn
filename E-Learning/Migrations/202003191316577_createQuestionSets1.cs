namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createQuestionSets1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionSets", "DateCreated", c => c.DateTime());
            AddColumn("dbo.QuestionSets", "UserModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionSets", "UserModified");
            DropColumn("dbo.QuestionSets", "DateCreated");
        }
    }
}
