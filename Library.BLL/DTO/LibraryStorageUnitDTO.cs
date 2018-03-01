namespace Library.BLL.DTO
{
    public class LibraryStorageUnitDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UnitName { get; set; }
        public int AutorId { get; set; }
        public AutorDTO Autor { get; set; }
    }
}