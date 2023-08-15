using CodingWikiWeb.DataAccess.Data;
using CodingWikiWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingWIkiWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.AsNoTracking().ToList();
            return View(categoryList);
        }

        public IActionResult Upsert(int? id)
        {
            Category obj = new();
            
            if(id is null || id.Equals(0))
            {
                //create
                return View(obj);
            }
            else
            {
                obj = _db.Categories.FirstOrDefault(x => x.CategoryId == id);

                if(obj is null)
                    return NotFound();
                else
                    return View(obj);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category obj)
        {
            if(ModelState.IsValid)
            {
                if(obj.CategoryId.Equals(0))
                {
                    //create
                    await _db.Categories.AddAsync(obj);
                }
                else
                {
                    //update
                    _db.Categories.Update(obj);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category obj = new();
            obj = _db.Categories.FirstOrDefault(x => x.CategoryId == id);

            if (id.Equals(0))
                return NotFound();

            _db.Categories.Remove(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateMultiple2()
        {
            List<Category> categories = new();
            for (int x = 1; x <= 2; x++)
                categories.Add(new Category { CategoryName = Guid.NewGuid().ToString() });

            _db.Categories.AddRange(categories);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateMultiple5()
        {
            List<Category> categories = new();
            for (int x = 1; x <= 5; x++)
                categories.Add(new Category { CategoryName = Guid.NewGuid().ToString() });

            _db.Categories.AddRange(categories);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveMultiple2()
        {
            IEnumerable<Category> categories = await _db.Categories.OrderByDescending(x => x.CategoryId).Take(2).ToListAsync();
            _db.Categories.RemoveRange(categories);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveMultiple5()
        {
            IEnumerable<Category> categories = await _db.Categories.OrderByDescending(x => x.CategoryId).Take(5).ToListAsync();
            _db.Categories.RemoveRange(categories);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
