using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "What is the name of your book?")]
        public string Title { get; set; }
        [Required(ErrorMessage = "When is your book printed?")]
        public int ReleaseDate { get; set; }        
        public BookGenre Genre { get; set; }
        public int AutorId { get; set; }        
        public Autor Autor { get; set; }
    }
}
