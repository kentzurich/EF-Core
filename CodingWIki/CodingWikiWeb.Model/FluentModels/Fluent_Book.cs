using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingWikiWeb.Model
{
    public class Fluent_Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public string PriceRange { get; set; }

        public virtual Fluent_BookDetail BookDetails { get; set; }
        public int Publisher_Id { get; set; }
        public virtual Fluent_Publisher Publishers { get; set; }
        //public List<Fluent_Author> Fluent_Authors { get; set; }
        public virtual List<Fluent_BookAuthorMap> Fluent_BookAuthorMap { get; set; }
    }
}
