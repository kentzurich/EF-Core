using CodingWikiWeb.DataAccess.Data;
using CodingWikiWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CodingWIkiWeb.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PublisherController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Publisher> publisher = await _db.Publishers.ToListAsync();
            return View(publisher);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Publisher publisher = new();

            if (id is null || id.Equals(0))
                return View(publisher);

            publisher = await _db.Publishers.FirstOrDefaultAsync(x => x.Publisher_Id == id);

            if(publisher is null)
                return NotFound();


            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Publisher obj)
        {
            if(ModelState.IsValid)
            {
                if (obj.Publisher_Id.Equals(0))
                    _db.Publishers.Add(obj);
                else
                    _db.Publishers.Update(obj);

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Publisher publisher = await _db.Publishers.FirstOrDefaultAsync(x => x.Publisher_Id == id);
            _db.Publishers.Remove(publisher);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
