using System.ComponentModel.DataAnnotations;

namespace CodingWikiWeb.Model.StoredProcAndView
{
    public class MainBookDetails
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
    }
}
