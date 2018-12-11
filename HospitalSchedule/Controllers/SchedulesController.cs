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
    public class SchedulesController : Controller
    {
        private readonly HospitalScheduleDbContext _context;

        public SchedulesController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var hospitalScheduleDbContext = _context.Schedule.Include(s => s.Nurse)
                .Include(s => s.OperationBlock_Shifts)
                .Include(s => s.OperationBlock_Shifts.Shift)
                .Include(s => s.OperationBlock_Shifts.OperationBlock);
            return View(await hospitalScheduleDbContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Nurse)
                .Include(s => s.OperationBlock_Shifts)
                .Include(s => s.OperationBlock_Shifts.Shift)
                .Include(s => s.OperationBlock_Shifts.OperationBlock)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["NurseId"] = new SelectList(_context.Nurse, "NurseId", "Name"); //Alterar Cellphonenumber para Name
            ViewData["OperationBlock_ShiftsId"] = new SelectList(_context.OperationBlock_Shifts, "OperationBlock_ShiftsId", "OperationBlock_ShiftsId");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,Date,NurseId,OperationBlock_ShiftsId")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NurseId"] = new SelectList(_context.Nurse, "NurseId", "Name", schedule.NurseId);
            ViewData["OperationBlock_ShiftsId"] = new SelectList(_context.OperationBlock_Shifts, "OperationBlock_ShiftsId", "OperationBlock_ShiftsId", schedule.OperationBlock_ShiftsId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["NurseId"] = new SelectList(_context.Nurse, "NurseId", "CellPhoneNumber", schedule.NurseId);
            ViewData["OperationBlock_ShiftsId"] = new SelectList(_context.OperationBlock_Shifts, "OperationBlock_ShiftsId", "OperationBlock_ShiftsId", schedule.OperationBlock_ShiftsId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,Date,NurseId,OperationBlock_ShiftsId")] Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleId))
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
            ViewData["NurseId"] = new SelectList(_context.Nurse, "NurseId", "CellPhoneNumber", schedule.NurseId);
            ViewData["OperationBlock_ShiftsId"] = new SelectList(_context.OperationBlock_Shifts, "OperationBlock_ShiftsId", "OperationBlock_ShiftsId", schedule.OperationBlock_ShiftsId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Nurse)
                .Include(s => s.OperationBlock_Shifts)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            _context.Schedule.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.ScheduleId == id);
        }
    }
}
