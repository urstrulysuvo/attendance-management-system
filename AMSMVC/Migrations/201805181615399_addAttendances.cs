namespace AMSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAttendances : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attandances", "StudentId", "dbo.Students");
            DropIndex("dbo.Attandances", new[] { "StudentId" });
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        AttendanceId = c.Long(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        AttandanceDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AttendanceId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            DropTable("dbo.Attandances");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Attandances",
                c => new
                    {
                        AttandanceId = c.Long(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        AttandanceDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AttandanceId);
            
            DropForeignKey("dbo.Attendances", "StudentId", "dbo.Students");
            DropIndex("dbo.Attendances", new[] { "StudentId" });
            DropTable("dbo.Attendances");
            CreateIndex("dbo.Attandances", "StudentId");
            AddForeignKey("dbo.Attandances", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
        }
    }
}
