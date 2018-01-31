using Library.WEB.EnumsViewModel;

namespace Library.WEB.Models
{
    public class MagazineViewModel
    {
        public int Id { get; set; }
        public int ReleaseDate { get; set; }
        public int IssueNumber { get; set; }
        public StylesOfPublicationsViewModel Style { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnitViewModel Unit { get; set; }
    }
}