namespace LibraryDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditAutorsForPeriodicalUnits : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Autors", "FoundingDate", c => c.Int(nullable: false));
            AlterColumn("dbo.Autors", "Surname", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Autors", "Surname", c => c.String(nullable: false));
            DropColumn("dbo.Autors", "FoundingDate");
        }
    }
}
