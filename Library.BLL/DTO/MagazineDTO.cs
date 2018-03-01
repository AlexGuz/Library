using Library.BLL.EnumsDTO;

namespace Library.BLL.DTO
{
    public class MagazineDTO
    {
        public int Id { get; set; }
        public int ReleaseDate { get; set; }
        public int IssueNumber { get; set; }
        public StylesOfPublicationsDTO Style { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnitDTO Unit { get; set; }
    }
}