namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createstudent2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Courses", newName: "Course");
            RenameTable(name: "dbo.TopicLists", newName: "TopicList");
            RenameTable(name: "dbo.Students", newName: "Student");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Student", newName: "Students");
            RenameTable(name: "dbo.TopicList", newName: "TopicLists");
            RenameTable(name: "dbo.Course", newName: "Courses");
        }
    }
}
