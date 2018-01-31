using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DAL.Models
{
    public class Autor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "What is the name of your author?")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<LibraryStorageUnit> Units { get; set; }
        public string AutorName { get { return $"{Name} {Surname}"; } }
        public int FoundingDate { get; set; }

        public Autor()
        {
            Units = new List<LibraryStorageUnit>();
        }
        public override string ToString()
        {
            return AutorName;
        }
    }
}