namespace Wingman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "NumberOfKeys", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "NumberOfRatings", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "NumberOfAnswersPicked", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "NumberOfAnswersSubmitted", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "NumberOfAnswersSubmitted", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "NumberOfAnswersPicked", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "NumberOfRatings", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "NumberOfKeys", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
        }
    }
}
