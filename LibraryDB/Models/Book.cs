﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDB.Models
{
    [Serializable]
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "When is your book printed?")]
        public int ReleaseDate { get; set; }
        public BookGenre Genre { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnit Unit { get; set; }

        public Book()
        {
        }
    }
}