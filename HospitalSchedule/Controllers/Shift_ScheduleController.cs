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
    [RequireHttps]
    public class Shift_ScheduleController : Controller
    {
        private readonly HospitalScheduleDbContext _context;

        public Shift_ScheduleController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Shift_Schedule
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shift_Schedule.ToListAsync());
        }

        // GET: Shift_Schedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift_Schedule = await _context.Shift_Schedule
                .FirstOrDefaultAsync(m => m.Shift_ScheduleID == id);
            if (shift_Schedule == null)
            {
                return NotFound();
            }

            return View(shift_Schedule);
        }

        // GET: Shift_Schedule/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shift_Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Shift_ScheduleID,ShiftDate,ScheduleFK,ShiftFK")] Shift_Schedule shift_Schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shift_Schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shift_Schedule);
        }

        // GET: Shift_Schedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift_Schedule = await _context.Shift_Schedule.FindAsync(id);
            if (shift_Schedule == null)
            {
                return NotFound();
            }
            return View(shift_Schedule);
        }

        // POST: Shift_Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Shift_ScheduleID,ShiftDate,ScheduleFK,ShiftFK")] Shift_Schedule shift_Schedule)
        {
            if (id != shift_Schedule.Shift_ScheduleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift_Schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Shift_ScheduleExists(shift_Schedule.Shift_ScheduleID))
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
            return View(shift_Schedule);
        }

        // GET: Shift_Schedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift_Schedule = await _context.Shift_Schedule
                .FirstOrDefaultAsync(m => m.Shift_ScheduleID == id);
            if (shift_Schedule == null)
            {
                return NotFound();
            }

            return View(shift_Schedule);
        }

        // POST: Shift_Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shift_Schedule = await _context.Shift_Schedule.FindAsync(id);
            _context.Shift_Schedule.Remove(shift_Schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Shift_ScheduleExists(int id)
        {
            return _context.Shift_Schedule.Any(e => e.Shift_ScheduleID == id);
        }
    }
}
