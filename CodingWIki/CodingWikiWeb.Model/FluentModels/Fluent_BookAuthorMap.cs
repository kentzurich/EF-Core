using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingWikiWeb.Model
{
    public class Fluent_BookAuthorMap
    {
        public int Book_Id { get; set; }
        public int Author_Id { get; set; }
        public virtual Fluent_Book Book { get; set; }
        public virtual Fluent_Author Author { get; set; }
    }
}
