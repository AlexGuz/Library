using Library.WEB.EnumsViewModel;

namespace Library.WEB.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public int ReleaseDate { get; set; }
        public BookGenreViewModel Genre { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnitViewModel Unit { get; set; }
    }
}