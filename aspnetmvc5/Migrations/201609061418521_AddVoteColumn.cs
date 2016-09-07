namespace aspnetmvc5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoteColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Vote", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Vote");
        }
    }
}
