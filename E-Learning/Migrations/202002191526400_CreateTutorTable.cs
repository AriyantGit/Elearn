namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTutorTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TutorDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fname = c.String(nullable: false),
                        Lname = c.String(),
                        Email = c.String(nullable: false),
                        PhoneNo = c.String(nullable: false, maxLength: 10),
                        Imageurl = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TutorDetails");
        }
    }
}
