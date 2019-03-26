namespace AMSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAttandances : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.AttandanceId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attandances", "StudentId", "dbo.Students");
            DropIndex("dbo.Attandances", new[] { "StudentId" });
            DropTable("dbo.Attandances");
        }
    }
}
