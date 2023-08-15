using CodingWikiWeb.DataAccess.Data;
using CodingWikiWeb.Model;
using CodingWikiWeb.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodingWIkiWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Book> BookList = await _db.Books
                .Include(x => x.Publishers)
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .ToListAsync();
            //List<Book> BookList = await _db.Books.ToListAsync();

            //foreach (var obj in BookList)
            //{
            //    //obj.Publishers = await _db.Publishers.FindAsync(obj.Publisher_Id); // less efficient
            //    _db.Entry(obj).Reference(x => x.Publishers).Load(); //more efficient
            //    _db.Entry(obj).Collection(x => x.BookAuthors).Load(); //more efficient
            //    foreach (var bookAuth in obj.BookAuthors)
            //    {
            //        _db.Entry(bookAuth).Reference(x => x.Author).Load(); //more efficient
            //    }
            //}

            return View(BookList);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            BookVM obj = new();
            obj.PublisherList = (await _db.Publishers.ToListAsync()).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Publisher_Id.ToString()
            });

            if(id is null || id.Equals(0))
            {
                //create
                return View(obj);
            }
            else
            {
                obj.Book = await _db.Books.FirstOrDefaultAsync(x => x.BookId == id);

                if(obj is null)
                    return NotFound();
                else
                    return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(BookVM obj)
        {
            if(obj.Book.BookId.Equals(0))
            {
                //create
                await _db.Books.AddAsync(obj.Book);
            }
            else
            {
                //update
                _db.Books.Update(obj.Book);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id is null || id.Equals(0))
            {
                //create
                return NotFound();
            }
            else
            {
                BookDetail obj = new();

                obj = await _db.BookDetails.Include(x => x.Books).FirstOrDefaultAsync(x => x.Book_Id == id);
                return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(BookDetail obj)
        {
            if(obj.BookDetail_Id.Equals(0))
                //create
                await _db.BookDetails.AddAsync(obj);
            else
                //update
                _db.BookDetails.Update(obj);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            BookVM obj = new();
            obj.Book = _db.Books.FirstOrDefault(x => x.BookId == id);

            if (id.Equals(0))
                return NotFound();

            _db.Books.Remove(obj.Book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ManageAuthors(int id)
        {
            BookAuthorVM bookAuthorVM = new()
            {
                BookAuthorList = await _db.BookAuthorMaps
                                        .Include(x => x.Author)
                                        .Include(x => x.Book)
                                        .Where(x => x.Book_Id == id).ToListAsync(),
                BookAuthor = new()
                {
                    Book_Id = id
                },
                Book = await _db.Books.FirstOrDefaultAsync(x => x.BookId == id)
            };
            List<int> tempListOfAssignAuthors = bookAuthorVM.BookAuthorList.Select(x => x.Author_Id).ToList();

            //NOT in a clause
            //get all the authors whos id is not in tempListOfAssignAuthors

            var tempList = await _db.Authors.Where(x => !tempListOfAssignAuthors.Contains(x.Author_Id)).ToListAsync();
            bookAuthorVM.AuthorList = tempList.Select(x => new SelectListItem
            {
                Value = x.Author_Id.ToString(),
                Text = x.FullName
            });

            return View(bookAuthorVM);
        }

        [HttpPost]
        public async Task<IActionResult> ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if(!bookAuthorVM.BookAuthor.Book_Id.Equals(0) && !bookAuthorVM.BookAuthor.Author_Id.Equals(0))
            {
                await _db.BookAuthorMaps.AddAsync(bookAuthorVM.BookAuthor);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id });
            }

            return View(bookAuthorVM);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAuthors(BookAuthorVM bookAuthorVM, int authorId)
        {
            int bookId = bookAuthorVM.Book.BookId;
            BookAuthorMap bookAuthorMap = await _db.BookAuthorMaps.FirstOrDefaultAsync(
                x => x.Author_Id == authorId && x.Book_Id == bookId);

            _db.BookAuthorMaps.Remove(bookAuthorMap);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }

        public async Task<IActionResult> PlayGround()
        {
            //var bookTemp = await _db.Books.FirstOrDefaultAsync();
            //bookTemp.Price = 100;

            //var bookCollection = _db.Books; // query will not yet execute
            //decimal totalPrice = 0;

            //foreach (var book in bookCollection) // query will execute
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = await _db.Books.ToListAsync(); // query will execute
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _db.Books; // query will not yet execute
            //var bookCount1 = bookCollection2.Count(); // query will execute

            //var bookCount2 = _db.Books.Count(); // query will execute

            //IEnumerable<Book> bookList1 = _db.Books;
            //var filteredBook1 = bookList1.Where(x => x.Price > 50).ToList();

            //IQueryable<Book> bookList2 = _db.Books;
            //var filteredBook2 = await bookList2.Where(x => x.Price > 50).ToListAsync();

            //var category = await _db.Categories.FirstOrDefaultAsync();
            //_db.Entry(category).State = EntityState.Modified;
            //await _db.SaveChangesAsync();

            ////updating related data
            //var bookDetails1 = await _db.BookDetails.Include(x => x.Books).FirstOrDefaultAsync(x => x.BookDetail_Id == 5);
            //bookDetails1.NumberOfChapters = 2222;
            //bookDetails1.NumberOfPages = 222;
            //_db.BookDetails.Update(bookDetails1);
            //await _db.SaveChangesAsync();

            //var bookDetails2 = await _db.BookDetails.Include(x => x.Books).FirstOrDefaultAsync(x => x.BookDetail_Id == 5);
            //bookDetails2.NumberOfChapters = 1111;
            //bookDetails2.NumberOfPages = 111;
            //_db.BookDetails.Attach(bookDetails2);
            //await _db.SaveChangesAsync();

            //view
            // column name must match with the dbset
            // need to match with db set
            var viewList = await _db.MainBookDetails.ToListAsync();
            var viewList1 = await _db.MainBookDetails.FirstOrDefaultAsync();
            var viewList2 = await _db.MainBookDetails.Where(x => x.Price > 30).ToListAsync();

            //raw sql
            var bookRaw = await _db.Books.FromSqlRaw($"select * from dbo.Books").ToListAsync();
            int id = 1;
            var bookRaw1= await _db.Books.FromSqlInterpolated($"select * from dbo.Books where bookid={id}").ToListAsync();
            //var bookRaw2 = await _db.Books.FromSqlRaw($"select * from dbo.Books={0}", 1).ToListAsync();

            //stored proc
            // need to match with db set
            var bookProc = await _db.Books.FromSqlInterpolated($"EXEC dbo.getBookDetails {id}").ToListAsync(); 

            return RedirectToAction(nameof(Index));
        }
    }
}
