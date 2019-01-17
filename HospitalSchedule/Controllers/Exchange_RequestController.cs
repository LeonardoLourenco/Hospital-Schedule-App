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
            var hospitalScheduleDbContext = _context.Exchange_Request //Muito provavelmente adicionar mais includes para poder ver todo o horário
                .Include(e => e.Schedule_Exchange1)
                .Include(e => e.Schedule_Exchange2)
                .Include(a => a.Schedule_Exchange1.Schedule)
                .Include(a => a.Schedule_Exchange2.Schedule)
                .Include(a => a.Schedule_Exchange1.Schedule.Nurse)
                .Include(a => a.Schedule_Exchange2.Schedule.Nurse)
                .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts)
                .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts)
                .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts.Shift)
                .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts.Shift)
                .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts.OperationBlock)
                .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts.OperationBlock);
                
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
                .Include(a => a.Schedule_Exchange1.Schedule)
                .Include(a => a.Schedule_Exchange2.Schedule)
                .Include(a => a.Schedule_Exchange1.Schedule.Nurse)
                .Include(a => a.Schedule_Exchange2.Schedule.Nurse)
                .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts)
                .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts)
                .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts.Shift)
                .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts.Shift)
                .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts.OperationBlock)
                .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts.OperationBlock)
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
            //if (ModelState.IsValid)
            //{
            //Preenche automaticamente ao criar

                var sched1 = _context.Schedule_Exchange1
                    .Where(Schedule_Exchange1 => Schedule_Exchange1.ScheduleId
                    == Convert.ToInt32(TempData["SchedEx1"]));
                var schedex1 = sched1.First();
                var sched2 = _context.Schedule_Exchange2
                    .Where(Schedule_Exchange2 => Schedule_Exchange2.ScheduleId
                    == Convert.ToInt32(TempData["SchedEx2"]));
                var schedex2 = sched2.First();

                exchange_Request.Schedule_Exchange1Id = schedex1.Schedule_Exchange1Id;
                exchange_Request.Schedule_Exchange2Id = schedex2.Schedule_Exchange2Id;
                exchange_Request.RequestState = "Pending";
                exchange_Request.Date_Exchange_Request = DateTime.Now;
                exchange_Request.Date_RequestState = DateTime.Now;


                TempData["Success"] = "The Exchange Request has been created successfully";
                _context.Add(exchange_Request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["Schedule_Exchange1Id"] = new SelectList(_context.Schedule_Exchange1, "Schedule_Exchange1Id", "Schedule_Exchange1Id", exchange_Request.Schedule_Exchange1Id);
            //ViewData["Schedule_Exchange2Id"] = new SelectList(_context.Schedule_Exchange2, "Schedule_Exchange2Id", "Schedule_Exchange2Id", exchange_Request.Schedule_Exchange2Id);
            //return View(exchange_Request);
        }

        // GET: Exchange_Request/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exchange_Request = await _context.Exchange_Request
                .Include(e => e.Schedule_Exchange1)
                .Include(e => e.Schedule_Exchange2)
                .Include(a => a.Schedule_Exchange1.Schedule)
                .Include(a => a.Schedule_Exchange2.Schedule)
                .Include(a => a.Schedule_Exchange1.Schedule.Nurse)
                .Include(a => a.Schedule_Exchange2.Schedule.Nurse)
                .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts)
                .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts)
                .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts.Shift)
                .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts.Shift)
                .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts.OperationBlock)
                .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts.OperationBlock)
                .FirstOrDefaultAsync(m => m.Exchange_RequestId == id);
                //.FindAsync(id);
            if (exchange_Request == null)
            {
                return NotFound();
            }
            ViewData["Schedule_Exchange1Id"] = new SelectList(_context.Schedule_Exchange1, "Schedule_Exchange1Id", "Schedule_Exchange1Id", exchange_Request.Schedule_Exchange1Id);
            ViewData["Schedule_Exchange2Id"] = new SelectList(_context.Schedule_Exchange2, "Schedule_Exchange2Id", "Schedule_Exchange2Id", exchange_Request.Schedule_Exchange2Id);

            //var nurse1 = await _context.Nurse.FindAsync(exchange_Request.Schedule_Exchange2.Schedule.NurseId);
            //var nurse2 = await _context.Nurse.FindAsync(exchange_Request.Schedule_Exchange1.ScheduleId);
            //var date1 = await _context.Nurse.FindAsync(exchange_Request.Schedule_Exchange1.Schedule.Date);
            //var date2 = await _context.Nurse.FindAsync(exchange_Request.Schedule_Exchange1.Schedule.Date);
            //TempData["Date1"] = "" + date1.Name;
            //TempData["Nurse1"] = "" + nurse1.Name;
            //TempData["Date2"]= "" + date2.Name;
            //TempData["Nurse2"]= "" + nurse2.Name;
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
                    exchange_Request.Date_RequestState = DateTime.Now;  //Coloca a data no momento na data em que foi alterado o estado

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
                TempData["Success"] = "The Exchange Request has been edited successfully";
                // Arranjar forma de colocar o enfermeiro 1 e 2 e a data 1 e 2 no edit.
                if (exchange_Request.RequestState == "Approved") //Se o pedido for aprovado, envia para o Schedule o id do 1º Horário e o id do 2º.
                {
                    
                    var schedule1id = await _context.Schedule_Exchange1.FindAsync(exchange_Request.Schedule_Exchange1Id);
                    var schedule2id = await _context.Schedule_Exchange2.FindAsync(exchange_Request.Schedule_Exchange2Id);
                    TempData["Schedule1"] = "" + schedule1id.ScheduleId;
                    TempData["Schedule2"] = "" + schedule2id.ScheduleId;
                    return RedirectToAction("Index", "Schedules");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Schedule_Exchange1Id"] = new SelectList(_context.Schedule_Exchange1, "Schedule_Exchange1Id", "Schedule_Exchange1Id", exchange_Request.Schedule_Exchange1Id);
            ViewData["Schedule_Exchange2Id"] = new SelectList(_context.Schedule_Exchange2, "Schedule_Exchange2Id", "Schedule_Exchange2Id", exchange_Request.Schedule_Exchange2Id);
            return View(exchange_Request);
        }

        // GET: Exchange_Request/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var exchange_Request = await _context.Exchange_Request
        //        .Include(e => e.Schedule_Exchange1)
        //        .Include(e => e.Schedule_Exchange2)
        //        .Include(a => a.Schedule_Exchange1.Schedule)
        //        .Include(a => a.Schedule_Exchange2.Schedule)
        //        .Include(a => a.Schedule_Exchange1.Schedule.Nurse)
        //        .Include(a => a.Schedule_Exchange2.Schedule.Nurse)
        //        .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts)
        //        .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts)
        //        .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts.Shift)
        //        .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts.Shift)
        //        .Include(a => a.Schedule_Exchange1.Schedule.OperationBlock_Shifts.OperationBlock)
        //        .Include(a => a.Schedule_Exchange2.Schedule.OperationBlock_Shifts.OperationBlock)
        //        .FirstOrDefaultAsync(m => m.Exchange_RequestId == id);
        //    if (exchange_Request == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(exchange_Request);
        //}

        //// POST: Exchange_Request/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var exchange_Request = await _context.Exchange_Request.FindAsync(id);
        //    _context.Exchange_Request.Remove(exchange_Request);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool Exchange_RequestExists(int id)
        {
            return _context.Exchange_Request.Any(e => e.Exchange_RequestId == id);
        }
    }
}
