namespace Library.WEB.ViewModels
{
    public class BrochureViewModel
    {
        public int Id { get; set; }
        public int ReleaseDate { get; set; }
        public string Type { get; set; }
        public int UnitId { get; set; }
        public int AutorId { get; set; }
        public string Title { get; set; }
        public AutorViewModel Autor { get; set; }
    }
}