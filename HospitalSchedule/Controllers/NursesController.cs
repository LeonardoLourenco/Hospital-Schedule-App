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
    public class NursesController : Controller
    {
        private readonly HospitalScheduleDbContext _context;
        public  int PageSize = 3;
        private List<Nurse> nurseList;

        public NursesController(HospitalScheduleDbContext context)
        {
            _context = context;
        }


        // GET: Nurses
       public async Task<IActionResult> Index(int page = 1)
        {
            int numNurses = await _context.Nurse.CountAsync();


            var Nurse =  await _context.Nurse
                    .OrderBy(p => p.Name)
                    .Skip(PageSize * (page - 1))
                    .Take(PageSize)
                    .ToListAsync();

            return View(
                new NursesView
                {
                    Nurses = Nurse,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numNurses
                    }
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Index(string search, string sortOrder, int page = 1)
        {

            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "SpecialtyId", "Name");

            int numNurses = await _context.Nurse.CountAsync();


            if (String.IsNullOrEmpty(search))
            {
                ViewData["Searched"] = false;
                return View(new NursesView()
                {
                    Nurses = await _context.Nurse.ToListAsync(),
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = numNurses
                    }
                });
            }
            //se nao devolve a pesquisa
            ViewData["Searched"] = true;


           
            switch (sortOrder)
            {
                case "Name":
                     nurseList = await _context.Nurse.Where(a => a.Name.ToLower()
                                                       .Contains(search.ToLower()))
                                                       .ToListAsync();
                    PageSize = nurseList.Count();
                    nurseList.AsEnumerable();

                    break;

                case "Email":

                    nurseList = await _context.Nurse.Where(a => a.Email.ToLower()
                                                       .Contains(search.ToLower()))
                                                       .ToListAsync();
                    PageSize = nurseList.Count();
                    nurseList.AsEnumerable();

                    break;

                case "Type":

                    nurseList = await _context.Nurse.Where(a => a.Type.ToString().ToLower()
                                                       .Contains(search.ToLower()))
                                                       .ToListAsync();
                    PageSize = nurseList.Count();
                    nurseList.AsEnumerable();
                    break;

                case "CellPhoneNumber":

                    nurseList = await _context.Nurse.Where(a => a.CellPhoneNumber.ToString()
                                                       .ToLower()
                                                       .Contains(search.ToLower()))
                                                       .ToListAsync();
                    PageSize = nurseList.Count();
                    nurseList.AsEnumerable();
                    break;

                case "IDCard":

                    nurseList = await _context.Nurse.Where(a => a.IDCard.ToString()
                                                       .ToLower()
                                                       .Contains(search.ToLower()))
                                                       .ToListAsync();
                    PageSize = nurseList.Count();
                    nurseList.AsEnumerable();
                    break;

                case "BirthDate":

                    nurseList = await _context.Nurse.Where(a => a.BirthDate.ToString()
                                                       .ToLower()
                                                       .Contains(search.ToLower()))
                                                       .ToListAsync();
                    PageSize = nurseList.Count();
                    nurseList.AsEnumerable();
                    break;

                case "YoungestChildBirthDate":

                    nurseList = await _context.Nurse.Where(a => a.YoungestChildBirthDate.ToString()
                                                       .ToLower()
                                                       .Contains(search.ToLower()))
                                                       .ToListAsync();
                    PageSize = nurseList.Count();
                    nurseList.AsEnumerable();
                    break;

                case "Specialty":

                    nurseList = await _context.Nurse.Where(a => a.Specialty.Name
                                                       .ToLower()
                                                       .Contains(search.ToLower()))
                                                       .ToListAsync();
                    PageSize = nurseList.Count();
                    nurseList.AsEnumerable();
                    break;
            
            default:
                     nurseList = await _context.Nurse.Where(a => a.Name.ToLower()
                                               .Contains(search.ToLower()))
                                               .ToListAsync();//Lista por default
                    PageSize = nurseList.Count();
                    nurseList.AsEnumerable();
                    break;
            }


            return View(new NursesView()
            {
                

                Nurses = nurseList,
                //todo:Na view falta meter um campo para poder ser inserido uma order
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = nurseList.Count(),
                    TotalItems = numNurses
                }
            });
        }


        // GET: Nurses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse
                .Include(n => n.Specialty)
                .FirstOrDefaultAsync(m => m.NurseId == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // GET: Nurses/Create
        public IActionResult Create()
        {
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "SpecialtyId", "Name");
            return View();
        }

        // POST: Nurses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NurseId,Name,Email,Type,CellPhoneNumber,IDCard,BirthDate,YoungestChildBirthDate,SpecialtyId")] Nurse nurse)
        {


            if (ValidateNumeroDocumentoCC(nurse.IDCard))
            {
                return View(nurse);
            }

            if (ModelState.IsValid)
            {
              
                _context.Add(nurse);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The Nurses has been created successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "SpecialtyId", "Name", nurse.SpecialtyId);
          
            return View(nurse);
        }

        // GET: Nurses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse.FindAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "SpecialtyId", "Name", nurse.SpecialtyId);
            return View(nurse);
        }

        // POST: Nurses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NurseId,Name,Email,Type,CellPhoneNumber,IDCard,BirthDate,YoungestChildBirthDate,SpecialtyId")] Nurse nurse)
        {
            if (id != nurse.NurseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                ValidateNumeroDocumentoCC(nurse.IDCard);

                try
                {
                    _context.Update(nurse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NurseExists(nurse.NurseId))
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
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "SpecialtyId", "Name", nurse.SpecialtyId);
            return View(nurse);
        }

        // GET: Nurses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse
                .Include(n => n.Specialty)
                .FirstOrDefaultAsync(m => m.NurseId == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // POST: Nurses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nurse = await _context.Nurse.FindAsync(id);
            _context.Nurse.Remove(nurse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NurseExists(int id)
        {
            return _context.Nurse.Any(e => e.NurseId == id);
        }



        public bool ValidateNumeroDocumentoCC(string numeroDocumento)
        {
            int sum = 0;
            bool secondDigit = false;
            if (numeroDocumento.Length != 12)
                throw new ArgumentException("Tamanho inválido para número de documento.");
        for (int i = numeroDocumento.Length - 1; i >= 0; --i)
            {
                string upper = numeroDocumento.ToUpper();
                int valor = GetNumberFromChar(upper[i]);
                if (secondDigit)
                {
                    valor *= 2;
                    if (valor > 9)
                        valor -= 9;
                }
                sum += valor;
                secondDigit = !secondDigit;
            }
            return (sum % 10) == 0;
        }
        public int GetNumberFromChar(char letter)
        {
            switch (letter)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'A': return 10;
                case 'B': return 11;
                case 'C': return 12;
                case 'D': return 13;
                case 'E': return 14;
                case 'F': return 15;
                case 'G': return 16;
                case 'H': return 17;
                case 'I': return 18;
                case 'J': return 19;
                case 'K': return 20;
                case 'L': return 21;
                case 'M': return 22;
                case 'N': return 23;
                case 'O': return 24;
                case 'P': return 25;
                case 'Q': return 26;
                case 'R': return 27;
                case 'S': return 28;
                case 'T': return 29;
                case 'U': return 30;
                case 'V': return 31;
                case 'W': return 32;
                case 'X': return 33;
                case 'Y': return 34;
                case 'Z': return 35;
            }
            throw new ArgumentException("Valor inválido no número de documento.");
        }

    }
}
