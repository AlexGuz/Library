using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.DAL.Enums;

namespace Library.DAL.Models
{
    [Serializable]
    public class Brochure 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "When it's your periodical printed?")]
        public int ReleaseDate { get; set; }
        public BrochureType Type { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnit Unit { get; set; }

        public Brochure()
        {
        }
    }
}