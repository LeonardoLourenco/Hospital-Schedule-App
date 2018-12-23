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
        private int PageSize = 3;

        public SchedulesController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index(int page = 1)
        {
            int numSchedules = await _context.Schedule.CountAsync();

            //Muda no Index apos a troca ser aprovada
            if (Convert.ToInt32(TempData["Schedule2"]) > 0) // Verifica se o que está dentro da TempData Schedule - o id do 2º Horário é maior que 0
            {
                var schedule1 = await _context.Schedule.FindAsync(Convert.ToInt32(TempData["Schedule1"])); // O tempdata apenas aceita string,
                var schedule2 = await _context.Schedule.FindAsync(Convert.ToInt32(TempData["Schedule2"])); // e falha a converter fazendo cast para int - (int)
                                                                                                           // portanto usamos Convert.ToInt32(), desta forma conseguimos 
                int aux = schedule1.NurseId;                                                            // mudar dinamicamente os enfermeiros à qual a troca foi aprovada
                schedule1.NurseId = schedule2.NurseId;
                schedule2.NurseId = aux;

                _context.Update(schedule1);
                _context.Update(schedule2);
                await _context.SaveChangesAsync();
            }

            var Schedules = await _context.Schedule
                    .Include(a => a.Nurse)
                    .Include(a => a.OperationBlock_Shifts)
                    .Include(a => a.OperationBlock_Shifts.Shift)
                    .Include(a => a.OperationBlock_Shifts.OperationBlock)
                    .OrderBy(p => p.Nurse.Name)

                    .Skip(PageSize * (page - 1))
                    .Take(PageSize)
                    .ToListAsync();

            return View(
                new SchedulesView
                {
                    Schedules = Schedules,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numSchedules
                    }
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Index(string search, int[] checkboxresp, int page = 1)
        {
            //o controlador apenas recebe valores da string search e do checkboxresp quando o butão submit associado é premido.
            int numSchedules = await _context.Schedule.CountAsync();


            if (checkboxresp.Length > 1)
            {
                TempData["Alert"] = "You can only select one schedules to request an exchange per page";
                return View(new SchedulesView()
                {
                    Schedules = await _context.Schedule
                     .Include(a => a.Nurse)
                     .Include(a => a.OperationBlock_Shifts)
                     .Include(a => a.OperationBlock_Shifts.Shift)
                     .Include(a => a.OperationBlock_Shifts.OperationBlock)
                     .OrderBy(p => p.Nurse.Name)
                     .ToListAsync(),
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numSchedules
                    }
                });
            }

            
            if (TempData["SchedEx1"] == null)
            {
                if (checkboxresp.Length == 0)
                {
                    TempData["Success"] = "Please select the first schedules to request an exchange";
                    return View(new SchedulesView()
                    {
                        Schedules = await _context.Schedule
                         .Include(a => a.Nurse)
                         .Include(a => a.OperationBlock_Shifts)
                         .Include(a => a.OperationBlock_Shifts.Shift)
                         .Include(a => a.OperationBlock_Shifts.OperationBlock)
                         .OrderBy(p => p.Nurse.Name)
                         .ToListAsync(),
                        PagingInfo = new PagingInfo()
                        {
                            CurrentPage = page,
                            ItemsPerPage = PageSize,
                            TotalItems = numSchedules
                        }
                    });
                }

                //id para troca
                TempData["SchedEx1"] = checkboxresp[0].ToString();
                TempData["Success"] = "Please select the second schedules to request an exchange";
                return View(new SchedulesView()
                {
                    Schedules = await _context.Schedule
                     .Include(a => a.Nurse)
                     .Include(a => a.OperationBlock_Shifts)
                     .Include(a => a.OperationBlock_Shifts.Shift)
                     .Include(a => a.OperationBlock_Shifts.OperationBlock)
                     .OrderBy(p => p.Nurse.Name)
                     .ToListAsync(),
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numSchedules
                    }
                });
            }
            else if (TempData["SchedEx2"] == null)
            {
                //id para troca
                TempData["SchedEx2"] = checkboxresp[0].ToString();

                if(TempData["SchedEx1"].Equals(TempData["SchedEx2"])) 
                {
                    //Caso os ids dos Schedules sejam iguais estes não podem ser usados, não se pode fazer um pedido de troca para a mesma linha do horário.
                    TempData["Alert"] = "You can't select the same schedule!!";
                    TempData["SchedEx1"] = null;
                    TempData["SchedEx2"] = null;

                    return View(new SchedulesView()
                    {
                        Schedules = await _context.Schedule
                     .Include(a => a.Nurse)
                     .Include(a => a.OperationBlock_Shifts)
                     .Include(a => a.OperationBlock_Shifts.Shift)
                     .Include(a => a.OperationBlock_Shifts.OperationBlock)
                     .OrderBy(p => p.Nurse.Name)
                     .ToListAsync(),
                        PagingInfo = new PagingInfo()
                        {
                            CurrentPage = page,
                            ItemsPerPage = PageSize,
                            TotalItems = numSchedules
                        }
                    });
                }
            }

            if(TempData["SchedEx2"] != null)
            {



                //verificar se existe alguma linha na tabela Schedule_Exchange1 com o 1º id e na tabela Schedule_Exchange2 com o 2º id

                var sched1 = _context.Schedule_Exchange1
                    .Where(Schedule_Exchange1 => Schedule_Exchange1.ScheduleId
                    == Convert.ToInt32(TempData["SchedEx1"]));
                var sched2 = _context.Schedule_Exchange2
                    .Where(Schedule_Exchange2 => Schedule_Exchange2.ScheduleId
                    == Convert.ToInt32(TempData["SchedEx2"]));
                //Se não existir criar linha na tabela
                if (!sched1.Any())
                {
                    //Criação de linha em Schedule_Exchange1
                    Schedule_Exchange1 schedule_Exchange1 = new Schedule_Exchange1
                    {
                        ScheduleId = Convert.ToInt32(TempData["SchedEx1"])
                    };

                    _context.Add(schedule_Exchange1);
                    await _context.SaveChangesAsync();


                }
                if (!sched2.Any())
                {
                    //Criação de linha em Schedule_Exchange2
                    Schedule_Exchange2 schedule_Exchange2 = new Schedule_Exchange2
                    {
                        ScheduleId = Convert.ToInt32(TempData["SchedEx2"])
                    };

                    _context.Add(schedule_Exchange2);
                    await _context.SaveChangesAsync();

                }
                
                // Agora que temos ambos os ids em cada tabela entermédia correspondente 
                // podemos buscar os ids dessas tabelas e criar o pedido aqui
                // Chamamos o Sched1 e o Sched2 novamente no caso de alguma das tabelas entermedias tenho sido acabada de criar

                sched1 = _context.Schedule_Exchange1
                    .Where(Schedule_Exchange1 => Schedule_Exchange1.ScheduleId == Convert.ToInt32(TempData["SchedEx1"]));
                sched2 = _context.Schedule_Exchange2
                    .Where(Schedule_Exchange2 => Schedule_Exchange2.ScheduleId == Convert.ToInt32(TempData["SchedEx2"]));

                //Manda para o create para confirmar
                return RedirectToAction("Create", "Exchange_Request");

            }

            //se nao tiver nada na pesquisa retorna a view anterior
            if (String.IsNullOrEmpty(search))
            {
                ViewData["Searched"] = false;
                return View(new SchedulesView()
                {
                    Schedules = await _context.Schedule
                    .Include(a => a.Nurse)
                    .Include(a => a.OperationBlock_Shifts)
                    .Include(a => a.OperationBlock_Shifts.Shift)
                    .Include(a => a.OperationBlock_Shifts.OperationBlock)
                    .OrderBy(p => p.Nurse.Name)
                    .ToListAsync(),
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numSchedules
                    }
                });
            }
            //se nao devolve a pesquisa
            ViewData["Searched"] = true;
            return View(new SchedulesView()
            {
                Schedules = await _context.Schedule
                .Include(a => a.Nurse)
                .Include(a => a.OperationBlock_Shifts)
                .Include(a => a.OperationBlock_Shifts.Shift)
                .Include(a => a.OperationBlock_Shifts.OperationBlock)
                .Where(Schedule => Schedule.Nurse.Name.ToLower().Contains(search.ToLower()))
                .OrderBy(p => p.Nurse.Name)
                .ToListAsync(),
                PagingInfo = new PagingInfo()
                {

                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = numSchedules
                }
            });
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
            ViewData["NurseId"] = new SelectList(_context.Nurse, "NurseId", "Name");
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
                TempData["Success"] = "The Schedule has been created successfully";
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
            ViewData["NurseId"] = new SelectList(_context.Nurse, "NurseId", "Name", schedule.NurseId);
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
                TempData["Success"] = "The Schedule has been edited successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["NurseId"] = new SelectList(_context.Nurse, "NurseId", "Name", schedule.NurseId);
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
                .Include(s => s.OperationBlock_Shifts.Shift)
                .Include(s => s.OperationBlock_Shifts.OperationBlock)
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
