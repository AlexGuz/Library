namespace LibraryDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrochureToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brochures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseDate = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        UnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LibraryStorageUnits", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.UnitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Brochures", "UnitId", "dbo.LibraryStorageUnits");
            DropIndex("dbo.Brochures", new[] { "UnitId" });
            DropTable("dbo.Brochures");
        }
    }
}
