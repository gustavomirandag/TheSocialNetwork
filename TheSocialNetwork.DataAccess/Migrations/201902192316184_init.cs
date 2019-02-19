namespace TheSocialNetwork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Builders;

    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(),
                        SentDateTime = c.DateTime(nullable: false),
                        Recipient_Id = c.Guid(),
                        Sender_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Recipient_Id)
                .ForeignKey("dbo.Profiles", t => t.Sender_Id)
                .Index(t => t.Recipient_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        PhotoUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "Sender_Id", "dbo.Profiles");
            DropForeignKey("dbo.Notifications", "Recipient_Id", "dbo.Profiles");
            DropIndex("dbo.Notifications", new[] { "Sender_Id" });
            DropIndex("dbo.Notifications", new[] { "Recipient_Id" });
            DropTable("dbo.Profiles");
            DropTable("dbo.Notifications");
        }
    }
}
