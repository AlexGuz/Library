namespace Library.WEB.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public int ReleaseDate { get; set; }
        public string Genre { get; set; }
        public int UnitId { get; set; }
        public AutorViewModel Autor { get; set; }
        public string Title { get; set; }
    }
}