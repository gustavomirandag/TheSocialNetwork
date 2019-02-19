namespace TheSocialNetwork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profileaddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "Address");
        }
    }
}
