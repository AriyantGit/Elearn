namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCourseTable1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Clevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Clevel", c => c.String(nullable: false));
        }
    }
}
