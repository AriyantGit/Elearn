namespace E_Learning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcarttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        UserModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Coupons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Discount = c.Int(nullable: false),
                        Validity = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(),
                        UserModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.StudentCourseRegistrations", "CouponId", c => c.Int());
            CreateIndex("dbo.StudentCourseRegistrations", "CouponId");
            AddForeignKey("dbo.StudentCourseRegistrations", "CouponId", "dbo.Coupons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartDetails", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCourseRegistrations", "CouponId", "dbo.Coupons");
            DropForeignKey("dbo.CartDetails", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentCourseRegistrations", new[] { "CouponId" });
            DropIndex("dbo.CartDetails", new[] { "StudentId" });
            DropIndex("dbo.CartDetails", new[] { "CourseId" });
            DropColumn("dbo.StudentCourseRegistrations", "CouponId");
            DropTable("dbo.Coupons");
            DropTable("dbo.CartDetails");
        }
    }
}
