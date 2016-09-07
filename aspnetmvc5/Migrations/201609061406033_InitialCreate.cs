namespace aspnetmvc5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        TopicID = c.Int(nullable: false),
                        Answer = c.String(),
                        UserID = c.String(),
                        DateCreated = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Topics", t => t.TopicID, cascadeDelete: true)
                .Index(t => t.TopicID);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        UserID = c.String(),
                        DateCreated = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.TopicID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "TopicID", "dbo.Topics");
            DropIndex("dbo.Comments", new[] { "TopicID" });
            DropTable("dbo.Topics");
            DropTable("dbo.Comments");
        }
    }
}
