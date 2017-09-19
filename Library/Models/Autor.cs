using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "What is the name of your author?")]
        public string Name { get; set; }
        [Required(ErrorMessage = "What is the surname of your author?")]
        public string Surname { get; set; }
        public ICollection<Book> Books { get; set; }
        public string AutorName { get { return $"{Name} {Surname}"; } }

        public Autor()
        {
            Books = new List<Book>();
        }
        public override string ToString()
        {
            return AutorName;
        }
    }
}