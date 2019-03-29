namespace TheSocialNetwork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(),
                        Post_Id = c.Guid(),
                        Sender_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .ForeignKey("dbo.Profiles", t => t.Sender_Id)
                .Index(t => t.Post_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProfileGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Group_Id = c.Guid(),
                        Profile_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id)
                .Index(t => t.Group_Id)
                .Index(t => t.Profile_Id);
            
            AddColumn("dbo.Posts", "Group_Id", c => c.Guid());
            CreateIndex("dbo.Posts", "Group_Id");
            AddForeignKey("dbo.Posts", "Group_Id", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileGroups", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.ProfileGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Comments", "Sender_Id", "dbo.Profiles");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Group_Id", "dbo.Groups");
            DropIndex("dbo.ProfileGroups", new[] { "Profile_Id" });
            DropIndex("dbo.ProfileGroups", new[] { "Group_Id" });
            DropIndex("dbo.Posts", new[] { "Group_Id" });
            DropIndex("dbo.Comments", new[] { "Sender_Id" });
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropColumn("dbo.Posts", "Group_Id");
            DropTable("dbo.ProfileGroups");
            DropTable("dbo.Groups");
            DropTable("dbo.Comments");
        }
    }
}
