using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalSchedule.Models;

namespace HospitalSchedule.Controllers
{
    public class SpecialtiesController : Controller
    {
        private readonly HospitalScheduleDbContext _context;
        public int PageSize = 3;

        public SpecialtiesController(HospitalScheduleDbContext context)
        {
            _context = context;
        }


        // GET: Specialties
        public async Task<IActionResult> Index(int page = 1)
        {
            int numSpecialty = await _context.Specialty.CountAsync();


            var Specialty = await
                _context.Specialty
                    .OrderBy(p => p.Name)
                   
                    .Skip(PageSize * (page - 1))
                    .Take(PageSize)
                    .ToListAsync();

            return View(
                new SpecialitiesView
                {
                    Specialties = Specialty,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numSpecialty
                    }
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int numSpecialty = await _context.Specialty.CountAsync();

         


            //se nao tiver nada na pesquisa retorna a view anterior
            if (String.IsNullOrEmpty(search))
            {
                ViewData["Searched"] = false;
                return View(new SpecialitiesView()
                {
                    Specialties = await _context.Specialty.ToListAsync(),
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numSpecialty
                    }
                });
            }
            //se nao devolve a pesquisa
            ViewData["Searched"] = true;
            return View(new SpecialitiesView()
            {
                Specialties = await _context.Specialty.Where(Specialty => Specialty.Name.ToLower().Contains(search.ToLower())).ToListAsync(),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = numSpecialty
                }
            });
        }



        // GET: Specialties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialty = await _context.Specialty
                .FirstOrDefaultAsync(m => m.SpecialtyId == id);
            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }

        // GET: Specialties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specialties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecialtyId,Name")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialty);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The Speciality has been created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(specialty);
        }

        // GET: Specialties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialty = await _context.Specialty.FindAsync(id);
            if (specialty == null)
            {
                return NotFound();
            }
            return View(specialty);
        }

        // POST: Specialties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecialtyId,Name")] Specialty specialty)
        {
            if (id != specialty.SpecialtyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialtyExists(specialty.SpecialtyId))
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
            return View(specialty);
        }

        // GET: Specialties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialty = await _context.Specialty
                .FirstOrDefaultAsync(m => m.SpecialtyId == id);
            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }

        // POST: Specialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialty = await _context.Specialty.FindAsync(id);
            _context.Specialty.Remove(specialty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialtyExists(int id)
        {
            return _context.Specialty.Any(e => e.SpecialtyId == id);
        }
    }
}
