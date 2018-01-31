namespace Library.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditBookEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "AutorId", "dbo.Autors");
            DropIndex("dbo.Books", new[] { "AutorId" });
            RenameColumn(table: "dbo.Books", name: "AutorId", newName: "Autor_Id");
            AddColumn("dbo.Books", "UnitId", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "Autor_Id", c => c.Int());
            CreateIndex("dbo.Books", "UnitId");
            CreateIndex("dbo.Books", "Autor_Id");
            AddForeignKey("dbo.Books", "UnitId", "dbo.LibraryStorageUnits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Books", "Autor_Id", "dbo.Autors", "Id");
            DropColumn("dbo.Books", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Title", c => c.String(nullable: false));
            DropForeignKey("dbo.Books", "Autor_Id", "dbo.Autors");
            DropForeignKey("dbo.Books", "UnitId", "dbo.LibraryStorageUnits");
            DropIndex("dbo.Books", new[] { "Autor_Id" });
            DropIndex("dbo.Books", new[] { "UnitId" });
            AlterColumn("dbo.Books", "Autor_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Books", "UnitId");
            RenameColumn(table: "dbo.Books", name: "Autor_Id", newName: "AutorId");
            CreateIndex("dbo.Books", "AutorId");
            AddForeignKey("dbo.Books", "AutorId", "dbo.Autors", "Id", cascadeDelete: true);
        }
    }
}
