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
    public class Schedule_Exchange1Controller : Controller
    {
        private readonly HospitalScheduleDbContext _context;

        public Schedule_Exchange1Controller(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Schedule_Exchange1
        public async Task<IActionResult> Index()
        {
            var hospitalScheduleDbContext = _context.Schedule_Exchange1
                .Include(s => s.Schedule);
            return View(await hospitalScheduleDbContext.ToListAsync());
        }

        // GET: Schedule_Exchange1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule_Exchange1 = await _context.Schedule_Exchange1
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.Schedule_Exchange1Id == id);
            if (schedule_Exchange1 == null)
            {
                return NotFound();
            }

            return View(schedule_Exchange1);
        }

        // GET: Schedule_Exchange1/Create
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId");
            return View();
        }

        // POST: Schedule_Exchange1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Schedule_Exchange1Id,ScheduleId")] Schedule_Exchange1 schedule_Exchange1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule_Exchange1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", schedule_Exchange1.ScheduleId);
            return View(schedule_Exchange1);
        }

        // GET: Schedule_Exchange1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule_Exchange1 = await _context.Schedule_Exchange1.FindAsync(id);
            if (schedule_Exchange1 == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", schedule_Exchange1.ScheduleId);
            return View(schedule_Exchange1);
        }

        // POST: Schedule_Exchange1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Schedule_Exchange1Id,ScheduleId")] Schedule_Exchange1 schedule_Exchange1)
        {
            if (id != schedule_Exchange1.Schedule_Exchange1Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule_Exchange1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Schedule_Exchange1Exists(schedule_Exchange1.Schedule_Exchange1Id))
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
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", schedule_Exchange1.ScheduleId);
            return View(schedule_Exchange1);
        }

        // GET: Schedule_Exchange1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule_Exchange1 = await _context.Schedule_Exchange1
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.Schedule_Exchange1Id == id);
            if (schedule_Exchange1 == null)
            {
                return NotFound();
            }

            return View(schedule_Exchange1);
        }

        // POST: Schedule_Exchange1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule_Exchange1 = await _context.Schedule_Exchange1.FindAsync(id);
            _context.Schedule_Exchange1.Remove(schedule_Exchange1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Schedule_Exchange1Exists(int id)
        {
            return _context.Schedule_Exchange1.Any(e => e.Schedule_Exchange1Id == id);
        }
    }
}
