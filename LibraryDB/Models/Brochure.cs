using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDB.Models
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