using System.Data.Entity;

namespace Library.Models
{
    public class LibraryContext: DbContext
    {
        public LibraryContext() : base("Library")
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Autor> Autors { get; set; }        
    }    
}