using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DAL.Models
{
    [Serializable]
    public class LibraryStorageUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string UnitName { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}