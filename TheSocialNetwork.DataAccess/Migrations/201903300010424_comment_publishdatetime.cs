namespace TheSocialNetwork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment_publishdatetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "PublishDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "PublishDateTime");
        }
    }
}
