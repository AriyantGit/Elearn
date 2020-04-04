namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnsrTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerDetails",
                c => new
                    {
                        AnswerDetailsId = c.Int(nullable: false, identity: true),
                        MarksObtain = c.Int(nullable: false),
                        percentage = c.Double(nullable: false),
                        TickAnsr = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Reshcdule = c.Boolean(nullable: false,defaultValue:false),
                        DateCreated = c.DateTime(),
                        UserModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.AnswerDetailsId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnswerDetails", "StudentId", "dbo.Students");
            DropForeignKey("dbo.AnswerDetails", "CourseId", "dbo.Courses");
            DropIndex("dbo.AnswerDetails", new[] { "CourseId" });
            DropIndex("dbo.AnswerDetails", new[] { "StudentId" });
            DropTable("dbo.AnswerDetails");
        }
    }
}
