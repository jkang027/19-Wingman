namespace Wingman.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadePropertiesDynamic : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WingmanUsers", "NumberOfAnswersPicked");
            DropColumn("dbo.WingmanUsers", "NumberOfAnswersSubmitted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WingmanUsers", "NumberOfAnswersSubmitted", c => c.Int());
            AddColumn("dbo.WingmanUsers", "NumberOfAnswersPicked", c => c.Int());
        }
    }
}
