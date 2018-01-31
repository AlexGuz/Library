using Library.BLL.EnumsDTO;

namespace Library.BLL.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public int ReleaseDate { get; set; }
        public BookGenreDTO Genre { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnitDTO Unit { get; set; }
    }
}