namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStuTeacherRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Role", c => c.String());
            AddColumn("dbo.TutorDetails", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TutorDetails", "Role");
            DropColumn("dbo.Students", "Role");
        }
    }
}
