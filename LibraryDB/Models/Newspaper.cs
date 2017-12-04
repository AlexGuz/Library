using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDB.Models
{
    [Serializable]
    public class Newspaper 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "When it's your periodical printed?")]
        public int ReleaseDate { get; set; }
        [Required(ErrorMessage = "What is the Issue Number of your newspaper?")]
        public int IssueNumber { get; set; }
        public NewspaperType Type { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnit Unit { get; set; }

        public Newspaper()
        {
            
        }
    }
}