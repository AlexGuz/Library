﻿using Library.DAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DAL.Models
{
    [Serializable]
    public class Newspaper
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReleaseDate { get; set; }
        public int IssueNumber { get; set; }
        public NewspaperType Type { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnit Unit { get; set; }

        public Newspaper()
        {

        }
    }
}