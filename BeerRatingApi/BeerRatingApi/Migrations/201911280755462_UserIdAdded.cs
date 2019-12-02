namespace BeerRatingApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beers", "UserId");
        }
    }
}
