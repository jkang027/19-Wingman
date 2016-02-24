namespace Wingman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenderNonNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int());
        }
    }
}
