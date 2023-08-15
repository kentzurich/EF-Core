using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingWikiWeb.Model
{
    public class BookAuthorMap
    {
        
        [ForeignKey(nameof(Book))]
        [Key]
        public int Book_Id { get; set; }
        [ForeignKey(nameof(Author))]
        [Key]
        public int Author_Id { get; set; }

        public virtual Book Book { get; set; } 
        public virtual Author Author { get; set; } 
    }
}
