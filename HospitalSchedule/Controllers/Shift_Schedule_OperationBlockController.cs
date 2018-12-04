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
    public class Shift_Schedule_OperationBlockController : Controller
    {
        private readonly HospitalScheduleDbContext _context;

        public Shift_Schedule_OperationBlockController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Shift_Schedule_OperationBlock
        public async Task<IActionResult> Index()
        {
            var hospitalScheduleDbContext = _context.Shift_Schedule_OperationBlock.Include(s => s.OperationBlock).Include(s => s.Schedule).Include(s => s.Shift);
            return View(await hospitalScheduleDbContext.ToListAsync());
        }

        // GET: Shift_Schedule_OperationBlock/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift_Schedule_OperationBlock = await _context.Shift_Schedule_OperationBlock
                .Include(s => s.OperationBlock)
                .Include(s => s.Schedule)
                .Include(s => s.Shift)
                .FirstOrDefaultAsync(m => m.Shift_Schedule_OperationBlockId == id);
            if (shift_Schedule_OperationBlock == null)
            {
                return NotFound();
            }

            return View(shift_Schedule_OperationBlock);
        }

        // GET: Shift_Schedule_OperationBlock/Create
        public IActionResult Create()
        {
            ViewData["OperationBlockId"] = new SelectList(_context.OperationBlock, "OperationBlockId", "BlockName");
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId");
            ViewData["ShiftId"] = new SelectList(_context.Shift, "ShiftId", "ShiftId");
            return View();
        }

        // POST: Shift_Schedule_OperationBlock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Shift_Schedule_OperationBlockId,ShiftDate,ScheduleId,ShiftId,OperationBlockId")] OperationBlock_Shift shift_Schedule_OperationBlock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shift_Schedule_OperationBlock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OperationBlockId"] = new SelectList(_context.OperationBlock, "OperationBlockId", "BlockName", shift_Schedule_OperationBlock.OperationBlockId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", shift_Schedule_OperationBlock.ScheduleId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "ShiftId", "ShiftId", shift_Schedule_OperationBlock.ShiftId);
            return View(shift_Schedule_OperationBlock);
        }

        // GET: Shift_Schedule_OperationBlock/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift_Schedule_OperationBlock = await _context.Shift_Schedule_OperationBlock.FindAsync(id);
            if (shift_Schedule_OperationBlock == null)
            {
                return NotFound();
            }
            ViewData["OperationBlockId"] = new SelectList(_context.OperationBlock, "OperationBlockId", "BlockName", shift_Schedule_OperationBlock.OperationBlockId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", shift_Schedule_OperationBlock.ScheduleId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "ShiftId", "ShiftId", shift_Schedule_OperationBlock.ShiftId);
            return View(shift_Schedule_OperationBlock);
        }

        // POST: Shift_Schedule_OperationBlock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Shift_Schedule_OperationBlockId,ShiftDate,ScheduleId,ShiftId,OperationBlockId")] OperationBlock_Shift shift_Schedule_OperationBlock)
        {
            if (id != shift_Schedule_OperationBlock.Shift_Schedule_OperationBlockId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift_Schedule_OperationBlock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Shift_Schedule_OperationBlockExists(shift_Schedule_OperationBlock.Shift_Schedule_OperationBlockId))
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
            ViewData["OperationBlockId"] = new SelectList(_context.OperationBlock, "OperationBlockId", "BlockName", shift_Schedule_OperationBlock.OperationBlockId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", shift_Schedule_OperationBlock.ScheduleId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "ShiftId", "ShiftId", shift_Schedule_OperationBlock.ShiftId);
            return View(shift_Schedule_OperationBlock);
        }

        // GET: Shift_Schedule_OperationBlock/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift_Schedule_OperationBlock = await _context.Shift_Schedule_OperationBlock
                .Include(s => s.OperationBlock)
                .Include(s => s.Schedule)
                .Include(s => s.Shift)
                .FirstOrDefaultAsync(m => m.Shift_Schedule_OperationBlockId == id);
            if (shift_Schedule_OperationBlock == null)
            {
                return NotFound();
            }

            return View(shift_Schedule_OperationBlock);
        }

        // POST: Shift_Schedule_OperationBlock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shift_Schedule_OperationBlock = await _context.Shift_Schedule_OperationBlock.FindAsync(id);
            _context.Shift_Schedule_OperationBlock.Remove(shift_Schedule_OperationBlock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Shift_Schedule_OperationBlockExists(int id)
        {
            return _context.Shift_Schedule_OperationBlock.Any(e => e.Shift_Schedule_OperationBlockId == id);
        }
    }
}
