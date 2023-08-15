using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodingWikiWeb.Model.ViewModel
{
    public class BookVM
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> PublisherList { get; set; }
    }
}
