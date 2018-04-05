using System.Collections.Generic;

namespace Library.BLL.DTO
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<LibraryStorageUnitDTO> Units { get; set; }
        public string AutorName { get { return $"{Name} {Surname}"; } }
        public int? FoundingDate { get; set; }

        public AutorDTO()
        {
            Units = new List<LibraryStorageUnitDTO>();
        }
    }
}