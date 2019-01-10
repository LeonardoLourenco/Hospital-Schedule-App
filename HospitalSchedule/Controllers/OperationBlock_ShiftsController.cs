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
    public class OperationBlock_ShiftsController : Controller
    {
        private readonly HospitalScheduleDbContext _context;
        private int PageSize = 3;

        public OperationBlock_ShiftsController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: OperationBlock_Shifts
        public async Task<IActionResult> Index(int page = 1)
        {
            int numOperationBlock_Shifts = await _context.OperationBlock_Shifts.CountAsync();


            var OperationBlock_Shifts = await _context.OperationBlock_Shifts
                    .Include(a => a.OperationBlock)
                    .Include(a => a.Shift )
                    .OrderBy(p => p.OperationBlock.BlockName)

                    .Skip(PageSize * (page - 1))
                    .Take(PageSize)
                    .ToListAsync();

            return View(
                new OperationBlock_ShiftView
                {
                    OperationBlock_Shifts = OperationBlock_Shifts,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numOperationBlock_Shifts
                    }
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int numOperationBlock_Shifts = await _context.OperationBlock_Shifts.CountAsync();

            //se nao tiver nada na pesquisa retorna a view anterior
            if (String.IsNullOrEmpty(search))
            {
                ViewData["Searched"] = false;
                return View(new OperationBlock_ShiftView()
                {
                    OperationBlock_Shifts = await _context.OperationBlock_Shifts
                    .Include(a => a.OperationBlock)
                    .Include(a => a.Shift)
                    .OrderBy(p => p.OperationBlock.BlockName)
                    .ToListAsync(),
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numOperationBlock_Shifts
                    }
                });
            }
            //se nao devolve a pesquisa
            ViewData["Searched"] = true;
            return View(new OperationBlock_ShiftView()
            {
                OperationBlock_Shifts = await _context.OperationBlock_Shifts
                .Include(a => a.OperationBlock)
                .Include(a => a.Shift)
                .Where(OperationBlock_Shifts => OperationBlock_Shifts.OperationBlock.BlockName.ToLower().Contains(search.ToLower()))
                .OrderBy(p => p.OperationBlock.BlockName)
                .ToListAsync(),
                PagingInfo = new PagingInfo()
                {

                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = numOperationBlock_Shifts
                }
            });
        }

        // GET: OperationBlock_Shifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationBlock_Shifts = await _context.OperationBlock_Shifts
                .Include(o => o.OperationBlock)
                .Include(o => o.Shift)
                .FirstOrDefaultAsync(m => m.OperationBlockId == id);
            if (operationBlock_Shifts == null)
            {
                return NotFound();
            }

            return View(operationBlock_Shifts);
        }

        // GET: OperationBlock_Shifts/Create
        public IActionResult Create()
        {
            ViewData["OperationBlockId"] = new SelectList(_context.OperationBlock, "OperationBlockId", "BlockName");
            ViewData["ShiftId"] = new SelectList(_context.Shift, "ShiftId", "ShiftName");
            return View();
        }

        // POST: OperationBlock_Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftId,OperationBlockId")] OperationBlock_Shifts operationBlock_Shifts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operationBlock_Shifts);
                await _context.SaveChangesAsync();
                var block = await _context.OperationBlock.FindAsync(operationBlock_Shifts.OperationBlockId); //Busca de forma explicita o bloco segundo o Id que temos do operationBlock_Shifts
                var shift = await _context.Shift.FindAsync(operationBlock_Shifts.OperationBlockId);          //Busca de forma explicita o turno segundo o Id que temos do operationBlock_Shifts          
                TempData["Success"] = "The connection between the Operation Block " + block.BlockName + " and the Shift " +
                    shift.ShiftName + " has been created successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["OperationBlockId"] = new SelectList(_context.OperationBlock, "OperationBlockId", "BlockName", operationBlock_Shifts.OperationBlockId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "ShiftId", "ShiftName", operationBlock_Shifts.ShiftId);
            return View(operationBlock_Shifts);
        }

        // GET: OperationBlock_Shifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationBlock_Shifts = await _context.OperationBlock_Shifts.FindAsync(id);
            if (operationBlock_Shifts == null)
            {
                return NotFound();
            }
            ViewData["OperationBlockId"] = new SelectList(_context.OperationBlock, "OperationBlockId", "BlockName", operationBlock_Shifts.OperationBlockId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "ShiftId", "ShiftName", operationBlock_Shifts.ShiftId);
            return View(operationBlock_Shifts);
        }

        // POST: OperationBlock_Shifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShiftId,OperationBlockId")] OperationBlock_Shifts operationBlock_Shifts)
        {
            if (id != operationBlock_Shifts.OperationBlockId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operationBlock_Shifts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationBlock_ShiftsExists(operationBlock_Shifts.OperationBlockId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var block = await _context.OperationBlock.FindAsync(operationBlock_Shifts.OperationBlockId); 
                var shift = await _context.Shift.FindAsync(operationBlock_Shifts.OperationBlockId);                   
                TempData["Success"] = "The connection between the Operation Block " + block.BlockName + " and the Shift " +
                    shift.ShiftName + " has been edited successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["OperationBlockId"] = new SelectList(_context.OperationBlock, "OperationBlockId", "BlockName", operationBlock_Shifts.OperationBlockId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "ShiftId", "ShiftName", operationBlock_Shifts.ShiftId);
            return View(operationBlock_Shifts);
        }

        // GET: OperationBlock_Shifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationBlock_Shifts = await _context.OperationBlock_Shifts
                .Include(o => o.OperationBlock)
                .Include(o => o.Shift)
                .FirstOrDefaultAsync(m => m.OperationBlockId == id);
            if (operationBlock_Shifts == null)
            {
                return NotFound();
            }

            return View(operationBlock_Shifts);
        }

        // POST: OperationBlock_Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operationBlock_Shifts = await _context.OperationBlock_Shifts.FindAsync(id);
            //Procurar se já há alguma ligação para a ligação Bloco Operatório - turno a eliminar
            var schedule = _context.Schedule.Where(Schedule => Schedule.OperationBlock_ShiftsId == operationBlock_Shifts.OperationBlock_ShiftsId);
            //Se existir pelo menos 1 ligação (Horário/Linha no horário com uma ligação bloco operatório - turno associada), o VS dará erro após guardar assincronamente,
            //nós queremos que apareça uma página de erro
            if (schedule.Any())
            {
                TempData["Error"] = "The operation block - shift connection that you are trying to delete is already connected to, at least, one schedule therefore you cant delete it.";
                return RedirectToAction(nameof(Error));
            }
            _context.OperationBlock_Shifts.Remove(operationBlock_Shifts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Error()
        {
            return View();
        }

        private bool OperationBlock_ShiftsExists(int id)
        {
            return _context.OperationBlock_Shifts.Any(e => e.OperationBlockId == id);
        }
    }
}
