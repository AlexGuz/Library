﻿using Library.DAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DAL.Models
{
    [Serializable]
    public class Magazine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "When it's your periodical printed?")]
        public int ReleaseDate { get; set; }
        [Required(ErrorMessage = "What is the Issue Number of your magazine?")]
        public int IssueNumber { get; set; }
        public StylesOfPublications Style { get; set; }
        public int UnitId { get; set; }
        public LibraryStorageUnit Unit { get; set; }

        public Magazine()
        {
        }
    }
}