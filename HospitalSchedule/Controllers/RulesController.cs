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
    public class RulesController : Controller
    {
        private readonly HospitalScheduleDbContext _context;
        private int PageSize = 3;

        public RulesController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Rules
        public async Task<IActionResult> Index(int page = 1)
        {
            int numRules = await _context.Rules.CountAsync();


            var Rules = await _context.Rules
                    .OrderBy(p => p.NurseAge)

                    .Skip(PageSize * (page - 1))
                    .Take(PageSize)
                    .ToListAsync();

            return View(
                new RulesView
                {
                    Rules = Rules,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numRules
                    }
                }
            );
        }
        [HttpPost]
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int numRules = await _context.Rules.CountAsync();

            //se nao tiver nada na pesquisa retorna a view anterior
            if (String.IsNullOrEmpty(search))
            {
                ViewData["Searched"] = false;
                return View(new RulesView()
                {
                    Rules = await _context.Rules
                    .OrderBy(p => p.NurseAge)
                    .ToListAsync(),
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numRules
                    }
                });
            }
            //se nao devolve a pesquisa
            ViewData["Searched"] = true;
            return View(new RulesView()
            {
                Rules = await _context.Rules
                .Where(rules => rules.WeeklyHours.ToString().Contains(search.ToLower()))
                .OrderBy(p => p.NurseAge)
                .ToListAsync(),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = numRules
                }
            });
        }
        /*
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
        }*/
        // GET: Rules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rules = await _context.Rules
                .FirstOrDefaultAsync(m => m.RulesId == id);
            if (rules == null)
            {
                return NotFound();
            }

            return View(rules);
        }

        // GET: Rules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RulesId,WeeklyHours,NurseAge,ChildAge,InBetweenShiftTime")] Rules rules)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rules);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The Rules have been created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(rules);
        }

        // GET: Rules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rules = await _context.Rules.FindAsync(id);
            if (rules == null)
            {
                return NotFound();
            }
            return View(rules);
        }

        // POST: Rules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RulesId,WeeklyHours,NurseAge,ChildAge,InBetweenShiftTime")] Rules rules)
        {
            if (id != rules.RulesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rules);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RulesExists(rules.RulesId))
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
            return View(rules);
        }

        // GET: Rules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rules = await _context.Rules
                .FirstOrDefaultAsync(m => m.RulesId == id);
            if (rules == null)
            {
                return NotFound();
            }

            return View(rules);
        }

        // POST: Rules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rules = await _context.Rules.FindAsync(id);
            _context.Rules.Remove(rules);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RulesExists(int id)
        {
            return _context.Rules.Any(e => e.RulesId == id);
        }
    }
}
