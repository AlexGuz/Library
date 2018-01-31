using Library.WEB.EnumsViewModel;

namespace Library.WEB.Models
{
    public class BrochureViewModel
    {
        public int Id { get; set; }
        public int ReleaseDate { get; set; }
        public BrochureTypeViewModel Type { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnitViewModel Unit { get; set; }
    }
}