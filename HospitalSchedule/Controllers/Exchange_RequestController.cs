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
    public class Exchange_RequestController : Controller
    {
        private readonly HospitalScheduleDbContext _context;

        public Exchange_RequestController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Exchange_Request
        public async Task<IActionResult> Index()
        {
            var hospitalScheduleDbContext = _context.Exchange_Request.Include(e => e.Schedule_Exchange1).Include(e => e.Schedule_Exchange2);
            return View(await hospitalScheduleDbContext.ToListAsync());
        }

        // GET: Exchange_Request/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exchange_Request = await _context.Exchange_Request
                .Include(e => e.Schedule_Exchange1)
                .Include(e => e.Schedule_Exchange2)
                .FirstOrDefaultAsync(m => m.Exchange_RequestId == id);
            if (exchange_Request == null)
            {
                return NotFound();
            }

            return View(exchange_Request);
        }

        // GET: Exchange_Request/Create
        public IActionResult Create()
        {
            ViewData["Schedule_Exchange1Id"] = new SelectList(_context.Schedule_Exchange1, "Schedule_Exchange1Id", "Schedule_Exchange1Id");
            ViewData["Schedule_Exchange2Id"] = new SelectList(_context.Schedule_Exchange2, "Schedule_Exchange2Id", "Schedule_Exchange2Id");
            return View();
        }

        // POST: Exchange_Request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Exchange_RequestId,Schedule_Exchange1Id,Schedule_Exchange2Id,RequestState,Date_RequestState,Date_Exchange_Request")] Exchange_Request exchange_Request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exchange_Request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Schedule_Exchange1Id"] = new SelectList(_context.Schedule_Exchange1, "Schedule_Exchange1Id", "Schedule_Exchange1Id", exchange_Request.Schedule_Exchange1Id);
            ViewData["Schedule_Exchange2Id"] = new SelectList(_context.Schedule_Exchange2, "Schedule_Exchange2Id", "Schedule_Exchange2Id", exchange_Request.Schedule_Exchange2Id);
            return View(exchange_Request);
        }

        // GET: Exchange_Request/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exchange_Request = await _context.Exchange_Request.FindAsync(id);
            if (exchange_Request == null)
            {
                return NotFound();
            }
            ViewData["Schedule_Exchange1Id"] = new SelectList(_context.Schedule_Exchange1, "Schedule_Exchange1Id", "Schedule_Exchange1Id", exchange_Request.Schedule_Exchange1Id);
            ViewData["Schedule_Exchange2Id"] = new SelectList(_context.Schedule_Exchange2, "Schedule_Exchange2Id", "Schedule_Exchange2Id", exchange_Request.Schedule_Exchange2Id);
            return View(exchange_Request);
        }

        // POST: Exchange_Request/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Exchange_RequestId,Schedule_Exchange1Id,Schedule_Exchange2Id,RequestState,Date_RequestState,Date_Exchange_Request")] Exchange_Request exchange_Request)
        {
            if (id != exchange_Request.Exchange_RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exchange_Request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exchange_RequestExists(exchange_Request.Exchange_RequestId))
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
            ViewData["Schedule_Exchange1Id"] = new SelectList(_context.Schedule_Exchange1, "Schedule_Exchange1Id", "Schedule_Exchange1Id", exchange_Request.Schedule_Exchange1Id);
            ViewData["Schedule_Exchange2Id"] = new SelectList(_context.Schedule_Exchange2, "Schedule_Exchange2Id", "Schedule_Exchange2Id", exchange_Request.Schedule_Exchange2Id);
            return View(exchange_Request);
        }

        // GET: Exchange_Request/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exchange_Request = await _context.Exchange_Request
                .Include(e => e.Schedule_Exchange1)
                .Include(e => e.Schedule_Exchange2)
                .FirstOrDefaultAsync(m => m.Exchange_RequestId == id);
            if (exchange_Request == null)
            {
                return NotFound();
            }

            return View(exchange_Request);
        }

        // POST: Exchange_Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exchange_Request = await _context.Exchange_Request.FindAsync(id);
            _context.Exchange_Request.Remove(exchange_Request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Exchange_RequestExists(int id)
        {
            return _context.Exchange_Request.Any(e => e.Exchange_RequestId == id);
        }
    }
}
