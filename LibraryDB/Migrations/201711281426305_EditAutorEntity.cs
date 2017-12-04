namespace LibraryDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditAutorEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Autor_Id", "dbo.Autors");
            DropIndex("dbo.Books", new[] { "Autor_Id" });
            DropColumn("dbo.Books", "Autor_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Autor_Id", c => c.Int());
            CreateIndex("dbo.Books", "Autor_Id");
            AddForeignKey("dbo.Books", "Autor_Id", "dbo.Autors", "Id");
        }
    }
}
