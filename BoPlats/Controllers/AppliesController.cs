using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoPlats.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

namespace BoPlats.Controllers
{
    public class AppliesController : Controller
    {
        private readonly DataContext_Milad _context;

        public AppliesController(DataContext_Milad context)
        {
            _context = context;
        }

        // GET
        [Authorize]
        public async Task<IActionResult> Index(string ApplicationAdress, string SearchString)
        {

            // getting Apaartment to list.. calling the class apartment and get the value Adress
            IQueryable<string> ApartmentAdressQuery = from a in _context.Apply
                                                      orderby a.Apartment.Adress
                                                      select a.Apartment.Adress;

            // getting all applications and including Apartment
            var applications = from m in _context.Apply.Include(a => a.Apartment)
                               select m;


            //checking if userinput contains any digits of an adress.
            if (!string.IsNullOrEmpty(SearchString))
            {
                applications = applications.Where(s => s.Apartment.Adress.Contains(SearchString));
            }

            // checking for application adress in list
            if (!string.IsNullOrEmpty(ApplicationAdress))
            {

                applications = applications.Where(x => x.Apartment.Adress == ApplicationAdress);
            }

            // caling view model
            var applicationDisplayVM = new ApplicationDisplayViewModel
            {
                ApartmentAdress = new SelectList(await ApartmentAdressQuery.Distinct().ToListAsync()),
                Applications = await applications.ToListAsync()

            };

            return View(applicationDisplayVM);

            //  var dataContext_Milad = _context.Apply.Include(a => a.Apartment);
            //   return View(await dataContext_Milad.ToListAsync());
        }

        // GET
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apply = await _context.Apply
                .Include(a => a.Apartment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apply == null)
            {
                return NotFound();
            }

            return View(apply);
        }


        public IActionResult Create()
        {
            ViewData["ApartmentForeginKey"] = new SelectList(_context.Apartment, "Id", "Adress");
            return View();
        }


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed([Bind("Id,Name,LastName,PhoneNumber,email,Salary,socSecNum,ApartmentForeginKey")] Apply apply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentForeginKey"] = new SelectList(_context.Apartment, "Id", "Adress", apply.ApartmentForeginKey);
            return View(apply);
        }

        // GET: Applies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apply = await _context.Apply.FindAsync(id);
            if (apply == null)
            {
                return NotFound();
            }
            ViewData["ApartmentForeginKey"] = new SelectList(_context.Apartment, "Id", "Adress", apply.ApartmentForeginKey);
            return View(apply);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(int id, [Bind("Id,Name,LastName,PhoneNumber,email,Salary,socSecNum,ApartmentForeginKey")] Apply apply)
        {
            if (id != apply.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplyExists(apply.Id))
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
            ViewData["ApartmentForeginKey"] = new SelectList(_context.Apartment, "Id", "Adress", apply.ApartmentForeginKey);
            return View(apply);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apply = await _context.Apply
                .Include(a => a.Apartment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apply == null)
            {
                return NotFound();
            }

            return View(apply);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apply = await _context.Apply.FindAsync(id);
            _context.Apply.Remove(apply);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplyExists(int id)
        {
            return _context.Apply.Any(e => e.Id == id);
        }
    }
}
