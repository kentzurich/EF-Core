﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingWikiWeb.Model
{
    public class BookDetail
    {
        [Key]
        public int BookDetail_Id { get; set; }
        [Required]
        public int NumberOfChapters { get; set; }
        public int NumberOfPages { get; set; }
        public string Weight { get; set; }
        [ForeignKey(nameof(Books))]
        public int Book_Id { get; set; }
        public virtual Book Books { get; set; }
    }
}
