namespace TheSocialNetwork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        FriendA = c.Guid(nullable: false),
                        FriendB = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.FriendA, t.FriendB })
                .ForeignKey("dbo.Profiles", t => t.FriendA)
                .ForeignKey("dbo.Profiles", t => t.FriendB)
                .Index(t => t.FriendA)
                .Index(t => t.FriendB);
            
            AddColumn("dbo.Profiles", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friendships", "FriendB", "dbo.Profiles");
            DropForeignKey("dbo.Friendships", "FriendA", "dbo.Profiles");
            DropIndex("dbo.Friendships", new[] { "FriendB" });
            DropIndex("dbo.Friendships", new[] { "FriendA" });
            DropColumn("dbo.Profiles", "Country");
            DropTable("dbo.Friendships");
        }
    }
}
