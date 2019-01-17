using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalSchedule.Models;
using System.Collections;
using System.Data.SqlClient;

namespace HospitalSchedule.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly HospitalScheduleDbContext _context;
        private int PageSize = 3;
        private int id;

        public SchedulesController(HospitalScheduleDbContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index(int page = 1)
        {
            int numSchedules = await _context.Schedule.CountAsync();


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
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int numSchedules = await _context.Schedule.CountAsync();

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
                .OrderBy(p => p.Nurse.Name)
                .Where(Schedule => Schedule.Nurse.Name.ToLower()
                .Contains(search.ToLower()))
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


        // GET: Schedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("ScheduleId,initialDate,NurseId,OperationBlock_ShiftsId")] Schedule schedule)
        {
            //Número de enfermeiros para preencher os turnos

            /*Validações*/
            //Só podem entrar datas a partir de uma segunda feira
             if (ModelState.IsValid)
            {
                    _context.Add(schedule);
                    DateTime date = schedule.initialDate;
                    DinamicSchedule(_context, date);
                TempData["Success"] = "The Schedule has been generated successfully";
                return RedirectToAction(nameof(Index));
          
            }
            
            return View(schedule);
        }


        private int[] NursesId()
        {
            var NurseIds = from nurse in _context.Nurse
                                  select nurse.NurseId;

            int[] arrayNurseIds = NurseIds.ToArray();
                

            return arrayNurseIds;
        }

        private int[] NursesWithChild_OR_HigherAged(int limitChildAge, int limitNurseAge)
        {
            var NurseIds = from nurse in _context.Nurse
                           where nurse.BirthDate.Year <= (DateTime.Now.Year - limitNurseAge)
                           select nurse.NurseId;

            int[] arrayNurseIds = NurseIds.ToArray();

            return arrayNurseIds;
        }


        private int[] NonRestrictedNurses(int limitChildAge, int limitNurseAge)//não têm filhos e não têm limite
        {
            var NurseIds = from nurse in _context.Nurse
                           where nurse.BirthDate.Year >= (DateTime.Now.Year - limitNurseAge)
                           select nurse.NurseId;

            int[] arrayNurseIds = NurseIds.ToArray();

            return arrayNurseIds;

         }


        private void DinamicSchedule(HospitalScheduleDbContext db, DateTime initialdate)
        {
            if (db == null)
            {
                db = _context;
            }
            //Rules and Variables
            string nurseInBetweenShiftTime = db.Rules.Where(a => a.RulesId == 1)
                                                 .OrderByDescending(a => a.InBetweenShiftTime)
                                                 .Select(a => a.InBetweenShiftTime)
                                                 .FirstOrDefault();

            int limitChildAge = db.Rules.Where(a => a.RulesId == 1)
                                                 .OrderByDescending(a => a.ChildAge)
                                                 .Select(a => a.ChildAge)
                                                 .FirstOrDefault();

            int limitNurseAge = db.Rules.Where(a => a.RulesId == 1)
                                                 .OrderByDescending(a => a.ChildAge)
                                                 .Select(a => a.NurseAge)
                                                 .FirstOrDefault();

           int nurseWeeklyHours = db.Rules.Where(a => a.RulesId == 1)
                                                .OrderByDescending(a => a.WeeklyHours)
                                                .Select(a => a.WeeklyHours)
                                                .FirstOrDefault();
            
            //Array com os Enf todos
            int[] Nurses = NursesId();

            //Array com os Enf que têm filhos menores e que têm menos do limite de idade para enfs
            int[] idRestrictedNurses = NursesWithChild_OR_HigherAged(limitChildAge, limitNurseAge);

            //Array com os Enf que têm filhos menores e que têm mais do limite de idade para enfs
            int[] idNonRestrictedNurses = NonRestrictedNurses(limitChildAge, limitNurseAge);



            List<int> NurseList = new List<int>(Nurses);//Lista de enfermeiros
            List<int> RestrictedNursesList ;//Lista para os enf s/filhos com menos do limite de idade
            List<int> NonRestrictedNursesList;//Lista para os enf c/filhos com mais do limite de idade


            Random rad = new Random();

            int choosenNurse = 0;//Enf escolhido
            int choosenShift = 0;//Turno escolhido


            //3.Obter a duração do turno
           int nShifts = 24/8;

            
            var startingHour = from shift in _context.Shift
                                    where shift.ShiftName == "Manha"
                                    select shift.StartingHour;
            String[] startingHourList = startingHour.ToArray();

            var startingHour1 = from shift in _context.Shift
                               where shift.ShiftName == "Tarde"
                               select shift.StartingHour;
            String[] startingHourList1 = startingHour1.ToArray();

            var startingHour2 = from shift in _context.Shift
                                where shift.ShiftName == "Noite"
                                select shift.StartingHour;
            String[] startingHourList2 = startingHour2.ToArray();



            //OperationBlock_Shifts que pertencem a turnos que são de manhã
            var notNigtShift1 = from x in _context.OperationBlock_Shifts
                                 where x.Shift.ShiftName == "Manha"
                                 select x.OperationBlockId;
            int[] notNigtShiftList = notNigtShift1.ToArray();

            var notNigtShift2 = from x in _context.OperationBlock_Shifts
                                where x.Shift.ShiftName == "Tarde"
                                 select x.OperationBlockId;
            int[] notNigtShiftList1 = notNigtShift2.ToArray();

            var nightShift = from x in _context.OperationBlock_Shifts
                             where x.Shift.ShiftName == "Noite"
                             select x.OperationBlockId;

            int[] nightShiftList = nightShift.ToArray();

            


            //OperationBlock_Shifts que pertencem a turnos que são de noite

            //Ciclo para uma semana e 4 vezes 
            for (int i = initialdate.Day; i <= initialdate.Day + 6; i++)
            {
                RestrictedNursesList = new List<int>(idRestrictedNurses);//Lista para os enf s/filhos com menos do limite de idade
                NonRestrictedNursesList = new List<int>(idNonRestrictedNurses);//Lista para os enf c/filhos com mais do limite de idade



                //REMOVER ENFS
                /*if (NonRestrictedNursesList.Count() != 0) //Noite
                {
                    if (startingHourList2[i] == "24:00") { 
                    choosenNurse = NonRestrictedNursesList[rad.Next(0, NonRestrictedNursesList.Count())];

                    choosenShift = nightShiftList[rad.Next(0, nightShiftList.Count())];
                    }
                }else if (NonRestrictedNursesList.Count() == 0)
                {

                }*/
                if (RestrictedNursesList.Count() != 0)
                {
                    if (startingHourList[i] =="08:00") { 
                    choosenNurse = RestrictedNursesList[rad.Next(0, RestrictedNursesList.Count())];
                    choosenShift = notNigtShiftList[rad.Next(0, notNigtShiftList.Count())];
                    }
                    else if(startingHourList1[i] == "16:00")
                    {
                        choosenNurse = RestrictedNursesList[rad.Next(0, RestrictedNursesList.Count())];
                        choosenShift = notNigtShiftList[rad.Next(0, notNigtShiftList.Count())];
                    }
                }

                _context.Schedule.Add(
               new Schedule { initialDate = initialdate, NurseId = choosenNurse, OperationBlock_ShiftsId = choosenShift }
               );
                //Remover o escolhido da lista
                //NonRestrictedNursesList.Remove(choosenNurse);
                RestrictedNursesList.Remove(choosenNurse);

                _context.SaveChanges();
            }

            int nurseAge = DateTime.Now.Year - db.Nurse.Select(a => a.BirthDate.Year).First();
            int childAge = nurseAge - db.Nurse.Select(a => a.YoungestChildBirthDate.Year).First();



            String blockName = db.OperationBlock_Shifts.Where(a => a.OperationBlockId == 1)
                                                   .Select(a => a.OperationBlock.BlockName).Single().ToString();



        }

      
    }
}





        /*
                 8.Fazer se:
             
                  8.1.Para cada enfermeiro, fazer:
                      8.1.1.Obter a idade e a idade do filho mais novo e o cargo//Pelo cálculo da data de nascimento do enfermeiro.
                          8.1.1.1.Se o enfermeiro tiver mais de x anos e ou  se o enfermeiro tiver o filho menor que x anos e ou se o enfermeiro for o enfermeiro-chefe , então:
                              8.1.1.1.1.Não faz turnos de noite.
                          8.1.1.2.Se não:
                              8.1.1.2.1.Escalona o enfermeiro num turno disponível(por ordem da hora do turno).*1

      9.Adicionar o nome do bloco no início da tabela.*/

    