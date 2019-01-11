using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalSchedule.Models;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace HospitalSchedule.Controllers
{
    [RequireHttps]
    public class NursesController : Controller
    {
        private readonly HospitalScheduleDbContext _context;
        public int PageSize = 3;

        public NursesController(HospitalScheduleDbContext context)
        {
            _context = context;
        }


        // GET: Nurses
        public async Task<IActionResult> Index(int page = 1)
        {
            int numNurses = await _context.Nurse.CountAsync();


            var Nurse = await 
                _context.Nurse
                    
                    .OrderBy(p => p.Name)
                    .Include(e => e.Specialty)
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
        public async Task<IActionResult> Index(string search,int page = 1)
        {
            int numNurses = await _context.Nurse.CountAsync();

            //se nao tiver nada na pesquisa retorna a view anterior
            if (String.IsNullOrEmpty(search))
            {
                ViewData["Searched"] = false;
                return View(new NursesView()
                {
                    Nurses = await _context.Nurse
                    .Include(n => n.Specialty)
                    .OrderBy(nurse => nurse.Name)
                    .ToListAsync(),
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
            return View(new NursesView()
            {
                Nurses = await _context.Nurse
                .Include(n => n.Specialty)
                .Where(nurse => nurse.Name.ToLower().Contains(search.ToLower()))
                .OrderBy(nurse => nurse.Name)
                .ToListAsync(),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
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

            var nCC = nurse.IDCard;
            var n_email = nurse.Email;
            DateTime Age = nurse.BirthDate;
            DateTime Child = nurse.YoungestChildBirthDate;



            //validaçoes de data de nascimento 
            if (YoungestChildBirthDateIsInvalid(Child))
            {
                ModelState.AddModelError("YoungestChildBirthDate", "YoungestChildBirthDate is invalid");
            }





            //validaçoes de data de nascimento 
            if (BirthDateIsInvalid(Age))
            {
                ModelState.AddModelError("BirthDate", "BirthDate is invalid");
            }


            //validaçoes de email na DataBase
            if (!emailIsValid(n_email))
            {
                ModelState.AddModelError("Email", "Email is invalid");
            }
            
            if (emailIsInvalid(n_email) ==true)
            {
                ModelState.AddModelError("Email", "email already exist");
            }

   

            //validaçao do ID na BD create
            if (!ValidateDocumentNumber(nCC))
            {
                ModelState.AddModelError("IDCard", "Number IDCard is invalid");

            }

            if(IDCardIsInvalid(nCC))
                {
                ModelState.AddModelError("IDCard", "Number IDCard already exist");
            }


            if (ModelState.IsValid)
            {

               if( ValidateDocumentNumber(nCC))
                _context.Add(nurse);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The Nurse "+ nurse.Name+" has been created successfully";
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
            var nCC = nurse.IDCard;
            var n_email = nurse.Email;




            //validaçoes de email na DataBase
            if (!emailIsValid(n_email))
            {
                ModelState.AddModelError("Email", "Email is invalid");
            }

            if (emailIsInvalid(n_email) == true)
            {
                ModelState.AddModelError("Email", "email already exist");
            }



            //validaçao do ID na BD create
            if (!ValidateDocumentNumber(nCC))
            {
                ModelState.AddModelError("IDCard", "Number IDCard is invalid");

            }

            if (IDCardIsInvalid(nCC))
            {
                ModelState.AddModelError("IDCard", "Number IDCard already exist");
            }

            if (id != nurse.NurseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                ValidateDocumentNumber(nurse.IDCard);

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
                TempData["Success"] = "The Nurse " + nurse.Name + " has been edited successfully";
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
            //Procurar se já há alguma ligação para o Enfermeiro a eliminar
            var schedule = _context.Schedule.Where(Schedule => Schedule.NurseId == nurse.NurseId);
            //Se existir pelo menos 1 ligação (Horário/Linha no horário com um enfermeiro associado), o VS dará erro após guardar assincronamente,
            //nós queremos que apareça uma página de erro
            if (schedule.Any())
            {
                TempData["Error"] = "The nurse that you are trying to delete is already connected to, at least, one schedule therefore you cant delete it.";
                return RedirectToAction(nameof(Error));
            }
            _context.Nurse.Remove(nurse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Error()
        {
            return View();
        }

        private bool NurseExists(int id)
        {
            return _context.Nurse.Any(e => e.NurseId == id);
        }



        private bool emailIsInvalid(string n_email)
        {
            bool IsInvalid = false;
            //Procura na BD se existem enfermeiros com o mesmo email
            var nurses = from e in _context.Nurse
                              where e.Email.Contains(n_email)
                              select e;

            if (!nurses.Count().Equals(0))
            {
                IsInvalid = true;
            }
            return IsInvalid;
        }




        //Função Data de nascimento do filho mais novo 
        private bool YoungestChildBirthDateIsInvalid(DateTime BirthDate)
        {
            bool IsInvalid = false;
            DateTime dateNow = DateTime.Now;

            if (dateNow.Year - BirthDate.Year < 0 || dateNow.Year - BirthDate.Year > 25)
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }


        //Função Data de nascimento 
        private bool BirthDateIsInvalid(DateTime BirthDate)
        {
            bool IsInvalid = false;
            DateTime dateNow = DateTime.Now;

            if (dateNow.Year - BirthDate.Year <= 18 || dateNow.Year - BirthDate.Year >80)
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }



        private bool IDCardIsInvalid(String cc)
        {
            bool IsInvalid = false;


            //Procura na BD se existem enfermeiros com o mesmo numero mecanografico
            var nurses = from e in _context.Nurse
                            where e.IDCard.Contains(cc)
                            select e;

            if (!nurses.Count().Equals(0))
            {
                IsInvalid = true;
            }

            return IsInvalid;
        }


        public static bool emailIsValid(string email)
        {
            string expression;
            if(email == null)
            {
                return false;
            }

            expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, string.Empty).Length == 0)
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





        public bool ValidateDocumentNumber(string DocumentNumber)
        {
            if (DocumentNumber == null)
            {
                return false;
            }
            int sum = 0;
            bool secondDigit = false;
            if (DocumentNumber.Length != 12)
                throw new ArgumentException("Tamanho inválido para número de documento.");
        for (int i = DocumentNumber.Length - 1; i >= 0; --i)
            {
                //string upper = numeroDocumento.ToUpper();
                int valor = GetNumberFromChar(DocumentNumber[i]);
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
