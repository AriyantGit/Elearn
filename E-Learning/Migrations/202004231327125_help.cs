namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class help : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Disable", c => c.String(defaultValue: "No"));
            AlterColumn("dbo.Coupons", "Disable", c => c.String(defaultValue: "No"));
            AlterColumn("dbo.TutorDetails", "Disable", c => c.String(defaultValue: "No"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TutorDetails", "Disable");
            DropColumn("dbo.Coupons", "Disable");
            DropColumn("dbo.Students", "Disable");
        }
    }
}
