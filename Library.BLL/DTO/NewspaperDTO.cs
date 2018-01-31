using Library.BLL.EnumsDTO;

namespace Library.BLL.DTO
{
    public class NewspaperDTO
    {
        public int Id { get; set; }
        public int ReleaseDate { get; set; }
        public int IssueNumber { get; set; }
        public NewspaperTypeDTO Type { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnitDTO Unit { get; set; }
    }
}