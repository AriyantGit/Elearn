namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createstudent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        FName = c.String(nullable: false),
                        LName = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        Image = c.Binary(),
                        DateCreated = c.DateTime(),
                        UserModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
