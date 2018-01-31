using Library.BLL.EnumsDTO;

namespace Library.BLL.DTO
{
    public class BrochureDTO 
    {
        public int Id { get; set; }
        public int ReleaseDate { get; set; }
        public BrochureTypeDTO Type { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnitDTO Unit { get; set; }
    }
}