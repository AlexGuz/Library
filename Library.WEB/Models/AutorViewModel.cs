using System.Collections.Generic;

namespace Library.WEB.Models
{
    public class AutorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<LibraryStorageUnitViewModel> Units { get; set; }
        public string AutorName { get { return $"{Name} {Surname}"; } }
        public int FoundingDate { get; set; }

        public AutorViewModel()
        {
            Units = new List<LibraryStorageUnitViewModel>();
        }
        public override string ToString()
        {
            return AutorName;
        }
    }
}