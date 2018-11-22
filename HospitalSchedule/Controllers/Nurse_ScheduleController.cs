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
    public class Nurse_ScheduleController : Controller
    {
        private readonly HospitalScheduleDbContext _context;

        public Nurse_ScheduleController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Nurse_Schedule
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nurses_Schedule.ToListAsync());
        }

        // GET: Nurse_Schedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse_Schedule = await _context.Nurses_Schedule
                .FirstOrDefaultAsync(m => m.Nurse_ScheduleID == id);
            if (nurse_Schedule == null)
            {
                return NotFound();
            }

            return View(nurse_Schedule);
        }

        // GET: Nurse_Schedule/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nurse_Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nurse_ScheduleID,ScheduleFK,NurseFK")] Nurse_Schedule nurse_Schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nurse_Schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nurse_Schedule);
        }

        // GET: Nurse_Schedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse_Schedule = await _context.Nurses_Schedule.FindAsync(id);
            if (nurse_Schedule == null)
            {
                return NotFound();
            }
            return View(nurse_Schedule);
        }

        // POST: Nurse_Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nurse_ScheduleID,ScheduleFK,NurseFK")] Nurse_Schedule nurse_Schedule)
        {
            if (id != nurse_Schedule.Nurse_ScheduleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nurse_Schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Nurse_ScheduleExists(nurse_Schedule.Nurse_ScheduleID))
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
            return View(nurse_Schedule);
        }

        // GET: Nurse_Schedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse_Schedule = await _context.Nurses_Schedule
                .FirstOrDefaultAsync(m => m.Nurse_ScheduleID == id);
            if (nurse_Schedule == null)
            {
                return NotFound();
            }

            return View(nurse_Schedule);
        }

        // POST: Nurse_Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nurse_Schedule = await _context.Nurses_Schedule.FindAsync(id);
            _context.Nurses_Schedule.Remove(nurse_Schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Nurse_ScheduleExists(int id)
        {
            return _context.Nurses_Schedule.Any(e => e.Nurse_ScheduleID == id);
        }
    }
}
