namespace Library.WEB.ViewModels
{
    public class MagazineViewModel
    {
        public int Id { get; set; }
        public int ReleaseDate { get; set; }
        public int IssueNumber { get; set; }
        public string Style { get; set; }
        public int UnitId { get; set; }
        public string Title { get; set; }
        public AutorViewModel Autor { get; set; }
    }
}