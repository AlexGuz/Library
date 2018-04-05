namespace Library.WEB.ViewModels
{
    public class AutorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AutorName { get { return $"{Name} {Surname}"; } }
        public int? FoundingDate { get; set; }
    }
}