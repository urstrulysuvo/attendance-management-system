namespace AMSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesubjectAllocators2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SubjectAllocators", "SubjectId4", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SubjectAllocators", "SubjectId4", c => c.Int(nullable: false));
        }
    }
}
