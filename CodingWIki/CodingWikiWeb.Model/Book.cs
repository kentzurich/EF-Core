using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingWikiWeb.Model
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        [MaxLength(20)]
        [Required]
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public string PriceRange { get; set; }

        public virtual BookDetail BookDetails { get; set; }

        [ForeignKey(nameof(Publishers))]
        public int Publisher_Id { get; set; }
        public virtual Publisher Publishers { get; set; }
        public virtual List<BookAuthorMap> BookAuthors { get; set; }
    }
}
