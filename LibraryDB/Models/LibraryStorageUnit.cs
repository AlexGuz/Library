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
    public class LibraryStorageUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "What is the name of your edition?")]
        public string Title { get; set; }
        public string UnitName { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}