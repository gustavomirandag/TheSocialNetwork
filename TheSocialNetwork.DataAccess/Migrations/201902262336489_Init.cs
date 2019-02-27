namespace TheSocialNetwork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
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
                        Address = c.String(),
                        PhotoUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhotoAlbums",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Profile_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id)
                .Index(t => t.Profile_Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContainerName = c.String(),
                        FileName = c.String(),
                        Url = c.String(),
                        ContentType = c.String(),
                        PhotoAlbum_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhotoAlbums", t => t.PhotoAlbum_Id)
                .Index(t => t.PhotoAlbum_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoAlbums", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.Photos", "PhotoAlbum_Id", "dbo.PhotoAlbums");
            DropForeignKey("dbo.Notifications", "Sender_Id", "dbo.Profiles");
            DropForeignKey("dbo.Notifications", "Recipient_Id", "dbo.Profiles");
            DropIndex("dbo.Photos", new[] { "PhotoAlbum_Id" });
            DropIndex("dbo.PhotoAlbums", new[] { "Profile_Id" });
            DropIndex("dbo.Notifications", new[] { "Sender_Id" });
            DropIndex("dbo.Notifications", new[] { "Recipient_Id" });
            DropTable("dbo.Photos");
            DropTable("dbo.PhotoAlbums");
            DropTable("dbo.Profiles");
            DropTable("dbo.Notifications");
        }
    }
}
