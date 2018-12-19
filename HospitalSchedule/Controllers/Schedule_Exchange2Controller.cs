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
    public class Schedule_Exchange2Controller : Controller
    {
        private readonly HospitalScheduleDbContext _context;

        public Schedule_Exchange2Controller(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Schedule_Exchange2
        public async Task<IActionResult> Index()
        {
            var hospitalScheduleDbContext = _context.Schedule_Exchange2.Include(s => s.Schedule);
            return View(await hospitalScheduleDbContext.ToListAsync());
        }

        // GET: Schedule_Exchange2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule_Exchange2 = await _context.Schedule_Exchange2
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.Schedule_Exchange2Id == id);
            if (schedule_Exchange2 == null)
            {
                return NotFound();
            }

            return View(schedule_Exchange2);
        }

        // GET: Schedule_Exchange2/Create
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId");
            return View();
        }

        // POST: Schedule_Exchange2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Schedule_Exchange2Id,ScheduleId")] Schedule_Exchange2 schedule_Exchange2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule_Exchange2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", schedule_Exchange2.ScheduleId);
            return View(schedule_Exchange2);
        }

        // GET: Schedule_Exchange2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule_Exchange2 = await _context.Schedule_Exchange2.FindAsync(id);
            if (schedule_Exchange2 == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", schedule_Exchange2.ScheduleId);
            return View(schedule_Exchange2);
        }

        // POST: Schedule_Exchange2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Schedule_Exchange2Id,ScheduleId")] Schedule_Exchange2 schedule_Exchange2)
        {
            if (id != schedule_Exchange2.Schedule_Exchange2Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule_Exchange2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Schedule_Exchange2Exists(schedule_Exchange2.Schedule_Exchange2Id))
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
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", schedule_Exchange2.ScheduleId);
            return View(schedule_Exchange2);
        }

        // GET: Schedule_Exchange2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule_Exchange2 = await _context.Schedule_Exchange2
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.Schedule_Exchange2Id == id);
            if (schedule_Exchange2 == null)
            {
                return NotFound();
            }

            return View(schedule_Exchange2);
        }

        // POST: Schedule_Exchange2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule_Exchange2 = await _context.Schedule_Exchange2.FindAsync(id);
            _context.Schedule_Exchange2.Remove(schedule_Exchange2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Schedule_Exchange2Exists(int id)
        {
            return _context.Schedule_Exchange2.Any(e => e.Schedule_Exchange2Id == id);
        }
    }
}
