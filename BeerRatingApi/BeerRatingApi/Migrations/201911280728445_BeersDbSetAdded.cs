namespace BeerRatingApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BeersDbSetAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Style = c.String(maxLength: 25),
                        Brewery = c.String(maxLength: 25),
                        Country = c.String(maxLength: 25),
                        Rating = c.Int(nullable: false),
                        Image = c.String(),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Beers");
        }
    }
}
