namespace LibraryDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLibraryStorageUnitToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LibraryStorageUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        UnitName = c.String(),
                        AutorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Autors", t => t.AutorId, cascadeDelete: true)
                .Index(t => t.AutorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LibraryStorageUnits", "AutorId", "dbo.Autors");
            DropIndex("dbo.LibraryStorageUnits", new[] { "AutorId" });
            DropTable("dbo.LibraryStorageUnits");
        }
    }
}
