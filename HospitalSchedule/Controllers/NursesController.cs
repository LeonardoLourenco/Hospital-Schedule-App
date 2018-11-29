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
    public class NursesController : Controller
    {
        private readonly HospitalScheduleDbContext _context;
        public int PageSize = 3;

        public NursesController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Nurses
        public async Task<IActionResult> Index(int page = 1)
        {
            int numNurses = await _context.Nurse.CountAsync();

          
            var Nurse = await
                _context.Nurse
                    .OrderBy(p => p.Name)
                    .Skip(PageSize * (page - 1))
                    .Take(PageSize)
                    .ToListAsync();

            return View(
                new NursesViewModel
                {
                    Nurses = Nurse,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numNurses
                    }
                }
            );
        }

    

        [HttpPost]
        public async Task<IActionResult> Index(string search)
        {
            //se nao tiver nada na pesquisa retorna a view anterior
            if (String.IsNullOrEmpty(search))
            {
                ViewData["Searched"] = false;
                return View(await _context.Nurse.ToListAsync());
            }
            //se nao devolve a pesquisa
            ViewData["Searched"] = true;
            return View(await _context.Nurse.Where(nurse => nurse.Name.ToLower().Contains(search.ToLower())).ToListAsync());
        }

        // GET: Nurses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse
                .FirstOrDefaultAsync(m => m.NurseID == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // GET: Nurses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nurses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NurseID,Name,BirthDate,CC,Type,Specialties,Email,CellPhoneNumber,YoungestChildBirthDate")] Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nurse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nurse);
        }

        // GET: Nurses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse.FindAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            return View(nurse);
        }

        // POST: Nurses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NurseID,Name,BirthDate,CC,Type,Specialties,Email,CellPhoneNumber,YoungestChildBirthDate")] Nurse nurse)
        {
            if (id != nurse.NurseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nurse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NurseExists(nurse.NurseID))
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
            return View(nurse);
        }

        // GET: Nurses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse
                .FirstOrDefaultAsync(m => m.NurseID == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // POST: Nurses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nurse = await _context.Nurse.FindAsync(id);
            _context.Nurse.Remove(nurse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NurseExists(int id)
        {
            return _context.Nurse.Any(e => e.NurseID == id);
        }
    }
}
