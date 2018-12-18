using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalSchedule.Models;
using System.Text.RegularExpressions;

namespace HospitalSchedule.Controllers
{
    [RequireHttps]
    public class ShiftsController : Controller
    {
        private readonly HospitalScheduleDbContext _context;

        public ShiftsController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Shifts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shift.ToListAsync());
        }

        // GET: Shifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift
                .FirstOrDefaultAsync(m => m.ShiftId == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: Shifts/Create
        public IActionResult Create()
        {
            return View();
        }

      

        // POST: Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftId,ShiftName,StartingHour,Duration")] Shift shift)
        {

            var name = shift.ShiftName;

            //validaçoes de email na DataBase
            if (!nameIsValid(name))
            {
                ModelState.AddModelError("ShiftName", "Shift is invalid");
            }


            if (nameIsInvalid(name) == true)
            {
                ModelState.AddModelError("ShiftName", "Shift already exist");
            }


            if (ModelState.IsValid)
            {
                _context.Add(shift);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The Shift " + shift.ShiftName + " has been created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(shift);
        }

        // GET: Shifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            return View(shift);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShiftId,ShiftName,StartingHour,Duration")] Shift shift)
        {

            var name = shift.ShiftName;

            //validaçoes de email na DataBase
            if (!nameIsValid(name))
            {
                ModelState.AddModelError("ShiftName", "Shift is invalid");
            }


            if (nameIsInvalid(name) == true)
            {
                ModelState.AddModelError("ShiftName", "Shift already exist");
            }

            if (id != shift.ShiftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.ShiftId))
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
            return View(shift);
        }

        // GET: Shifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift
                .FirstOrDefaultAsync(m => m.ShiftId == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shift = await _context.Shift.FindAsync(id);
            _context.Shift.Remove(shift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftExists(int id)
        {
            return _context.Shift.Any(e => e.ShiftId == id);
        }




        private bool nameIsInvalid(string name)
        {
            bool IsInvalid = false;
            //Procura na BD se existem turnos iguais
            var Shifts = from e in _context.Shift
                         where e.ShiftName.Contains(name)
                               select e;

            if (!Shifts.Count().Equals(0))
            {
                IsInvalid = true;
            }
            return IsInvalid;
        }



        public static bool nameIsValid(string name)
        {
            string expression;
            expression = "[a-zA-Z][a-zA-Z ]*";
            if (Regex.IsMatch(name, expression))
            {
                if (Regex.Replace(name, expression, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
