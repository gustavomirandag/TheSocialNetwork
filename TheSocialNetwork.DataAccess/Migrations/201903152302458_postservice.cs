namespace TheSocialNetwork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postservice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PublishDateTime = c.DateTime(nullable: false),
                        Content = c.String(),
                        Recipient_Id = c.Guid(),
                        Sender_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Recipient_Id)
                .ForeignKey("dbo.Profiles", t => t.Sender_Id)
                .Index(t => t.Recipient_Id)
                .Index(t => t.Sender_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Sender_Id", "dbo.Profiles");
            DropForeignKey("dbo.Posts", "Recipient_Id", "dbo.Profiles");
            DropIndex("dbo.Posts", new[] { "Sender_Id" });
            DropIndex("dbo.Posts", new[] { "Recipient_Id" });
            DropTable("dbo.Posts");
        }
    }
}
