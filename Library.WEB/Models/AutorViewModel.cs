using System.ComponentModel.DataAnnotations;

namespace Library.WEB.Models
{
    public class AutorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "What is the name of your author?")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AutorName { get { return $"{Name} {Surname}"; } }
        public int? FoundingDate { get; set; }
        
        public override string ToString()
        {
            return AutorName;
        }
    }
}