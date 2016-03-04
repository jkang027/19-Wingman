namespace Wingman.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ResponseId = c.Int(nullable: false, identity: true),
                        SubmissionId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ResponseText = c.String(),
                        Picked = c.Boolean(nullable: false),
                        DateSubmitted = c.DateTime(nullable: false),
                        KeyPrice = c.Int(nullable: false),
                        Purchased = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseId)
                .ForeignKey("dbo.Submissions", t => t.SubmissionId)
                .ForeignKey("dbo.WingmanUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.SubmissionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Submissions",
                c => new
                    {
                        SubmissionId = c.Int(nullable: false, identity: true),
                        TopicId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Context = c.String(),
                        TextMessage = c.String(),
                        DateOpened = c.DateTime(nullable: false),
                        DateClosed = c.DateTime(),
                    })
                .PrimaryKey(t => t.SubmissionId)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("dbo.WingmanUsers", t => t.UserId)
                .Index(t => t.TopicId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        TopicName = c.String(),
                    })
                .PrimaryKey(t => t.TopicId);
            
            CreateTable(
                "dbo.WingmanUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Email = c.String(),
                        Telephone = c.String(),
                        Gender = c.Int(nullable: false),
                        NumberOfFlags = c.Int(),
                        NumberOfKeys = c.Int(),
                        NumberOfRatings = c.Int(),
                        AverageRating = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.WingmanUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Submissions", "UserId", "dbo.WingmanUsers");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.WingmanUsers");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Responses", "UserId", "dbo.WingmanUsers");
            DropForeignKey("dbo.Submissions", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Responses", "SubmissionId", "dbo.Submissions");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Submissions", new[] { "UserId" });
            DropIndex("dbo.Submissions", new[] { "TopicId" });
            DropIndex("dbo.Responses", new[] { "UserId" });
            DropIndex("dbo.Responses", new[] { "SubmissionId" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.WingmanUsers");
            DropTable("dbo.Topics");
            DropTable("dbo.Submissions");
            DropTable("dbo.Responses");
        }
    }
}
