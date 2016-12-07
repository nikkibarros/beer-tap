namespace BeerTapExercise2.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BeerTaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Volume = c.Double(nullable: false),
                        BeerTapState = c.Int(nullable: false),
                        OfficeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Offices", t => t.OfficeId, cascadeDelete: true)
                .Index(t => t.OfficeId);
            
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BeerTaps", "OfficeId", "dbo.Offices");
            DropIndex("dbo.BeerTaps", new[] { "OfficeId" });
            DropTable("dbo.Offices");
            DropTable("dbo.BeerTaps");
        }
    }
}
