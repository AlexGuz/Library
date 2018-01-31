namespace Library.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMagazineToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Magazines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseDate = c.Int(nullable: false),
                        IssueNumber = c.Int(nullable: false),
                        Style = c.Int(nullable: false),
                        UnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LibraryStorageUnits", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.UnitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Magazines", "UnitId", "dbo.LibraryStorageUnits");
            DropIndex("dbo.Magazines", new[] { "UnitId" });
            DropTable("dbo.Magazines");
        }
    }
}
