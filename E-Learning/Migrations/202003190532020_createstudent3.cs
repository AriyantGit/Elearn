namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createstudent3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Course", newName: "Courses");
            RenameTable(name: "dbo.TopicList", newName: "TopicLists");
            RenameTable(name: "dbo.Student", newName: "Students");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Students", newName: "Student");
            RenameTable(name: "dbo.TopicLists", newName: "TopicList");
            RenameTable(name: "dbo.Courses", newName: "Course");
        }
    }
}
