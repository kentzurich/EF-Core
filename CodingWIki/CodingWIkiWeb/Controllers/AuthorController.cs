using CodingWikiWeb.DataAccess.Data;
using CodingWikiWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingWIkiWeb.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Author> author = await _db.Authors.ToListAsync();
            return View(author);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Author author = new();

            if (id is null || id.Equals(0))
                return View(author);

            author = await _db.Authors.FirstOrDefaultAsync(x => x.Author_Id == id);

            if (author is null)
                return NotFound();


            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Author obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Author_Id.Equals(0))
                    _db.Authors.Add(obj);
                else
                    _db.Authors.Update(obj);

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Author author = await _db.Authors.FirstOrDefaultAsync(x => x.Author_Id == id);
            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
