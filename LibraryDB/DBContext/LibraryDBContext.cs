using System.Data.Entity;
using LibraryDB.Models;

namespace LibraryDB
{
    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext() : base("Library")
        {
            Database.SetInitializer(new LibraryDBContextInitializer());
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Autor> Autors { get; set; }
        public DbSet<LibraryStorageUnit> LibraryStorageUnits { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Brochure> Brochures { get; set; }
        public DbSet<Newspaper> Newspapers { get; set; }
    }
}