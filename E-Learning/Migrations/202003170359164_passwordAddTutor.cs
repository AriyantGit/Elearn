namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passwordAddTutor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TutorDetails", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TutorDetails", "Password");
        }
    }
}
