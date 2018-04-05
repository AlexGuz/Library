namespace Library.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<LibraryDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "LibraryDB.LibraryDBContext";
        }

        protected override void Seed(LibraryDBContext context)
        {
            
        }
    }
}