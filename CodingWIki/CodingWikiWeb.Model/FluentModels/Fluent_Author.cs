using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingWikiWeb.Model
{
    public class Fluent_Author
    {
        public int Author_Id { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }
        public string FullName {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        //public List<Fluent_Book> Fluent_Books { get; set; }
        public virtual List<Fluent_BookAuthorMap> Fluent_BookAuthorMap { get; set; }
    }
}
