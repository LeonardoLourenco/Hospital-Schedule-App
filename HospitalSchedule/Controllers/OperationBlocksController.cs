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
    public class OperationBlocksController : Controller
    {
        private readonly HospitalScheduleDbContext _context;
        private int PageSize = 3;

        public OperationBlocksController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: OperationBlocks
        public async Task<IActionResult> Index(int page = 1)
        {
            int numOperationBlocks = await _context.OperationBlock.CountAsync();

            var OperationBlocks = await
                _context.OperationBlock
                .Include(e => e.OperationBlock_Shifts)
                .OrderBy(p => p.BlockName)

                .Skip(PageSize * (page - 1))
                .Take(PageSize)
                .ToListAsync();




            return View(
                new OperationBlockView
                {
                    OperationBlocks = OperationBlocks,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numOperationBlocks
                    }
                }
            );
        }


        [HttpPost]
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int numOperationBlocks = await _context.OperationBlock.CountAsync();

            //se nao tiver nada na pesquisa retorna a view anterior
            if (String.IsNullOrEmpty(search))
            {
                ViewData["Searched"] = false;
                return View(new OperationBlockView()
                {
                    OperationBlocks = await _context.OperationBlock.ToListAsync(),
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numOperationBlocks
                    }
                });
            }

            ViewData["Searched"] = true;
            return View(new OperationBlockView()
            {
                OperationBlocks = await _context.OperationBlock.Where(OperationBlock => OperationBlock.BlockName.ToLower().Contains(search.ToLower())).ToListAsync(),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = numOperationBlocks
                }
            });
        }
        // GET: OperationBlocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationBlock = await _context.OperationBlock
                .FirstOrDefaultAsync(m => m.OperationBlockId == id);
            if (operationBlock == null)
            {
                return NotFound();
            }

            return View(operationBlock);
        }

        // GET: OperationBlocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OperationBlocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OperationBlockId,BlockName")] OperationBlock operationBlock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operationBlock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(operationBlock);
        }

        // GET: OperationBlocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationBlock = await _context.OperationBlock.FindAsync(id);
            if (operationBlock == null)
            {
                return NotFound();
            }
            return View(operationBlock);
        }

        // POST: OperationBlocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OperationBlockId,BlockName")] OperationBlock operationBlock)
        {
            if (id != operationBlock.OperationBlockId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operationBlock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationBlockExists(operationBlock.OperationBlockId))
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
            return View(operationBlock);
        }

        // GET: OperationBlocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationBlock = await _context.OperationBlock
                .FirstOrDefaultAsync(m => m.OperationBlockId == id);
            if (operationBlock == null)
            {
                return NotFound();
            }

            return View(operationBlock);
        }

        // POST: OperationBlocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operationBlock = await _context.OperationBlock.FindAsync(id);
            _context.OperationBlock.Remove(operationBlock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationBlockExists(int id)
        {
            return _context.OperationBlock.Any(e => e.OperationBlockId == id);
        }
    }
}
