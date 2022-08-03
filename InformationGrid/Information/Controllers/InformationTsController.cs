using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Information.Data;
using Information.Models;

namespace Information.Controllers
{
    public class InformationTsController : Controller
    {
        public IConfiguration _configuration { get; }
        private  readonly InfoContext _context;

        public InformationTsController(InfoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: InformationTs
        public IActionResult Index()
        {
            //var infoContext = _context.InformationTs.Include(i => i.CityNavigation);
            ViewBag.Info = _context.InformationTs.Include(i => i.CityNavigation).ToList();
            return View(ViewBag.Info);
        }

        // GET: InformationTs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.InformationTs == null)
            {
                return NotFound();
            }

            var informationT = _context.InformationTs
                .Include(i => i.CityNavigation)
                .FirstOrDefault(m => m.Id == id);
            if (informationT == null)
            {
                return NotFound();
            }

            return View(informationT);
        }

        // GET: InformationTs/Create
        public IActionResult Create()
        {
            var city = _context.CityTs.ToList();
            ViewBag.city = new SelectList(_context.CityTs, "CityId", "CityName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InformationT informationT)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informationT);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.city = new SelectList(_context.CityTs, "CityId", "CityId", informationT.City);
            return View(informationT);
        }

        // GET: InformationTs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.InformationTs == null)
            {
                return NotFound();
            }

            var informationT =  _context.InformationTs.Find(id);
            if (informationT == null)
            {
                return NotFound();
            }
            ViewBag.city = new SelectList(_context.CityTs, "CityId", "CityName");
            return View(informationT);
        }

        // POST: InformationTs/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,InformationT informationT)
        {
            if (id != informationT.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.InformationTs.Update(informationT);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationTExists(informationT.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.city = new SelectList(_context.CityTs, "CityId", "CityId", informationT.City);
            return View(informationT);
        }

        // GET: InformationTs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InformationTs == null)
            {
                return NotFound();
            }

            var informationT = await _context.InformationTs
                .Include(i => i.CityNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (informationT == null)
            {
                return NotFound();
            }

            return View(informationT);
        }

        // POST: InformationTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.InformationTs == null)
            {
                return Problem("Entity set 'InfoContext.InformationTs'  is null.");
            }
            var informationT = _context.InformationTs.Find(id);
            if (informationT != null)
            {
                _context.InformationTs.Remove(informationT);
            }
            
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool InformationTExists(int id)
        {
          return (_context.InformationTs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
