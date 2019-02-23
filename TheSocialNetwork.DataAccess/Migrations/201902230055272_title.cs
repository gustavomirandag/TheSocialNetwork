namespace TheSocialNetwork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class title : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhotoAlbums", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhotoAlbums", "Title");
        }
    }
}
