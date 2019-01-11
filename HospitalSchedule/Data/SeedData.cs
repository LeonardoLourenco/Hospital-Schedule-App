using HospitalSchedule.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Data
{
    public class SeedData
    {
        const string ROLE_ADMINISTRATOR = "Administrator"; //Acesso a tudo
        const string ROLE_BLOCK_DIRETOR = "Block Diretor"; //Acesso respectivo
        const string ROLE_CHIEF_NURSE = "Chief Nurse"; //Acesso respectivo
        const string ROLE_NURSE = "Nurse"; //Acesso respectivo


        internal static void Populate(IServiceProvider applicationServices)
        {

            using (var serviceScope = applicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<HospitalScheduleDbContext>();


                SeedSpecialities(db);
                SeedOperationBlock(db);
                SeedShifts(db);
                SeedOperationBlock_Shifts(db);
                SeedNurses(db);
                SeedSchedule(db);
                SeedRules(db);
                
            }
        }

        private static async void MakeSureRoleExistsAsync(RoleManager<IdentityRole> roleManager, string role) //Verifica se o rele existe e cria-o
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));

            }
        }

        public static async Task CreateApplicationRolesandAdminUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string ADMIN_USER = "admin@noemail.com";
            const string ADMIN_PASSWORD = "sECRET$123";

            MakeSureRoleExistsAsync(roleManager, ROLE_ADMINISTRATOR);
            MakeSureRoleExistsAsync(roleManager, ROLE_BLOCK_DIRETOR);
            MakeSureRoleExistsAsync(roleManager, ROLE_CHIEF_NURSE);
            MakeSureRoleExistsAsync(roleManager, ROLE_NURSE);

            IdentityUser user = await userManager.FindByNameAsync(ADMIN_USER);
            if (user == null)
            {
                user = new IdentityUser { UserName = ADMIN_USER };
                await userManager.CreateAsync(user, ADMIN_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(user, ROLE_ADMINISTRATOR))
            {
                await userManager.AddToRoleAsync(user, ROLE_ADMINISTRATOR);
            }
        }

        public static async Task CreateApplicationBlockDiretorRoleandUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string BLOCK_DIRETOR_USER = "BlockDiretor@noemail.com";
            const string BLOCK_DIRETOR_PASSWORD = "IaMDir3to4";

            MakeSureRoleExistsAsync(roleManager, ROLE_ADMINISTRATOR);
            MakeSureRoleExistsAsync(roleManager, ROLE_BLOCK_DIRETOR);
            MakeSureRoleExistsAsync(roleManager, ROLE_CHIEF_NURSE);
            MakeSureRoleExistsAsync(roleManager, ROLE_NURSE);


            IdentityUser user = await userManager.FindByNameAsync(BLOCK_DIRETOR_USER);
            if (user == null)
            {
                user = new IdentityUser { UserName = BLOCK_DIRETOR_USER };
                await userManager.CreateAsync(user, BLOCK_DIRETOR_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(user, ROLE_BLOCK_DIRETOR))
            {
                await userManager.AddToRoleAsync(user, ROLE_BLOCK_DIRETOR);
            }
        }

        public static async Task CreateApplicationChiefNurseRoleandUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string CHIEF_NURSE_USER = "ChiefNurse@noemail.com";
            const string CHIEF_NURSE_PASSWORD = "IaMNu4s3CHEF";

            MakeSureRoleExistsAsync(roleManager, ROLE_ADMINISTRATOR);
            MakeSureRoleExistsAsync(roleManager, ROLE_BLOCK_DIRETOR);
            MakeSureRoleExistsAsync(roleManager, ROLE_CHIEF_NURSE);
            MakeSureRoleExistsAsync(roleManager, ROLE_NURSE);


            IdentityUser user = await userManager.FindByNameAsync(CHIEF_NURSE_USER);
            if (user == null)
            {
                user = new IdentityUser { UserName = CHIEF_NURSE_USER };
                await userManager.CreateAsync(user, CHIEF_NURSE_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(user, ROLE_CHIEF_NURSE))
            {
                await userManager.AddToRoleAsync(user, ROLE_CHIEF_NURSE);
            }
        }

        public static async Task CreateApplicationNurseRoleandUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string NURSE_USER = "1011881@ipg.pt";
            const string NURSE_PASSWORD = "IaMNu4s3";

            MakeSureRoleExistsAsync(roleManager, ROLE_ADMINISTRATOR);
            MakeSureRoleExistsAsync(roleManager, ROLE_BLOCK_DIRETOR);
            MakeSureRoleExistsAsync(roleManager, ROLE_CHIEF_NURSE);
            MakeSureRoleExistsAsync(roleManager, ROLE_NURSE);


            IdentityUser user = await userManager.FindByNameAsync(NURSE_USER);
            if (user == null)
            {
                user = new IdentityUser { UserName = NURSE_USER };
                await userManager.CreateAsync(user, NURSE_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(user, ROLE_NURSE))
            {
                await userManager.AddToRoleAsync(user, ROLE_NURSE);
            }
        }

        private static void SeedRules(HospitalScheduleDbContext db)
        {
            if (db.Rules.Any()) return;
            db.Rules.AddRange(
                  new Rules
                  {

                      WeeklyHours = 35,
                      NurseAge = 60,
                      ChildAge=5,
                      InBetweenShiftTime="16:00"
                  });

            db.SaveChanges();
        }

    

    private static void SeedSpecialities(HospitalScheduleDbContext db)
        {
            if (db.Specialty.Any()) return;

            db.Specialty.AddRange(
                  new Specialty
                  {
                      Name = "Cardiologia",
                  },
                  new Specialty
                  {
                      Name = "Genética médica",
                  },
                  new Specialty
                    {
                      Name = "Geriatria",
                    },
                   new Specialty
                   {
                       Name = "Homeopatia",
                   },
                  new Specialty
                  {
                      Name = "Infectologia",
                  },
                  new Specialty
                  {
                      Name = "Psiquiatria",
                  },
                  new Specialty
                  {
                      Name = "Urulogia",
                  });
            db.SaveChanges();
        }


        private static void SeedOperationBlock_Shifts(HospitalScheduleDbContext db)
        {
            if (db.OperationBlock_Shifts.Any()) return;
            db.OperationBlock_Shifts.AddRange(
                  new OperationBlock_Shifts
                  {

                      ShiftId = 1,
                      OperationBlockId = 1,

                  },
                  new OperationBlock_Shifts
                  {


                      ShiftId = 2,
                      OperationBlockId = 2,

                  },
                   new OperationBlock_Shifts
                   {

                       ShiftId = 3,
                       OperationBlockId = 3,

                   },
                  new OperationBlock_Shifts
                  {

                      ShiftId = 4,
                      OperationBlockId = 4,

                  },
                      new OperationBlock_Shifts
                      {


                          ShiftId = 5,
                          OperationBlockId = 5,

                      });

            db.SaveChanges();
        }


        private static void SeedShifts(HospitalScheduleDbContext db)
        {
            if (db.Shift.Any()) return;
            db.Shift.AddRange(
                  new Shift
                  {

                      ShiftName = "Tarde",
                      StartingHour = "08:00",
                      Duration = "08:00"
                  },
                  new Shift
                  {

                      ShiftName = "Tarde",
                      StartingHour = "08:00",
                      Duration = "08:00"
                  },
                   new Shift
                   {

                       ShiftName = "Noite",
                       StartingHour = "00:00",
                       Duration = "08:00"
                   },
                  new Shift
                  {

                      ShiftName = "Noite",
                      StartingHour = "12.00",
                      Duration = "08:00"
                  },
                      new Shift
                      {

                          ShiftName = "Tarde",
                          StartingHour = "16:00",
                          Duration = "08:00"

                      });

            db.SaveChanges();
        }



        private static void SeedOperationBlock(HospitalScheduleDbContext db)
        {
            if (db.OperationBlock.Any()) return;

            db.OperationBlock.AddRange(
                  new OperationBlock
                  {
                      BlockName = "bloco A",

                  },
                  new OperationBlock
                  {
                      BlockName = "bloco D",

                  },
                  new OperationBlock
                  {
                      BlockName = "bloco G",

                  },
                  new OperationBlock
                  {
                      BlockName = "bloco F",

                  },
                  new OperationBlock
                  {
                      BlockName = "bloco C",

                  });
            db.SaveChanges();
        }


        private static void SeedSchedule(HospitalScheduleDbContext db)
        {
            if (db.Schedule.Any()) return;

        
            //criei um objeto do tipo schedule onde vai returnar um elemento unico onde s do tipo shcedule tem como nome fabio...
          
            db.Schedule.AddRange(
                  new Schedule
                  {
                      Date = new DateTime(2018, 5, 1, 8, 30, 00),
                      NurseId = 1,
                      OperationBlock_ShiftsId = 1

                  },
                  new Schedule
                  {
                      Date = new DateTime(2018, 5, 1, 8, 30, 00),
                      NurseId = 2,
                      OperationBlock_ShiftsId = 2

                  });

            db.SaveChanges();
        }


        private static void SeedNurses(HospitalScheduleDbContext db)
        {
            if (db.Nurse.Any()) return;
            db.Nurse.AddRange(

                  new Nurse
                  {
                      Name = "Fabio Miguel Ambrosio",
                      BirthDate = new DateTime(1988, 5, 1),
                      IDCard = "22345576",
                      Type = 2,
                      SpecialtyId = 1,
                      Email = "1011881@ipg.pt",
                      CellPhoneNumber = "921234543",
                      YoungestChildBirthDate = DateTime.Parse("15-05-1987")
                  },
                  new Nurse
                  {
                      Name = "Maria Luiza Ambrosio",
                      BirthDate = new DateTime(1988, 5, 1),
                      IDCard = "22345576",
                      Type = 3,
                      SpecialtyId = 2,
                      Email = "1010381@ipg.pt",
                      CellPhoneNumber = "924354464",
                      YoungestChildBirthDate = DateTime.Parse("15-05-1987")
                  },
                  new Nurse
                  {
                      Name = "Zé Santos",
                      BirthDate = new DateTime(1977, 3, 1),
                      IDCard = "4231276",
                      Type = 2,
                      SpecialtyId = 6,
                      Email = "1111811@ipg.pt",
                      CellPhoneNumber = "921231143",
                      YoungestChildBirthDate = DateTime.Parse("11-05-2011")
                  },
                   new Nurse
                   {
                       Name = "Lara Luiza Fonseca",
                       BirthDate = new DateTime(1988, 5, 1),
                       IDCard = "22345576",
                       Type = 3,
                       SpecialtyId = 4,
                       Email = "1010381@ipg.pt",
                       CellPhoneNumber = "924352264",
                       YoungestChildBirthDate = DateTime.Parse("15-11-2000")
                   },
                  new Nurse
                  {
                      Name = "Manuel Alcides",
                      BirthDate = new DateTime(1978, 2, 1),
                      IDCard = "42321576",
                      Type = 1,
                      SpecialtyId = 3,
                      Email = "1111811@ipg.pt",
                      CellPhoneNumber = "921233343",
                      YoungestChildBirthDate = DateTime.Parse("11-05-1999")
                  },
                  new Nurse
                  {
                      Name = "Mara Andrade Gil",
                      BirthDate = new DateTime(1988, 5, 1),
                      IDCard = "228574576",
                      Type = 3,
                      SpecialtyId = 3,
                      Email = "101121@ipg.pt",
                      CellPhoneNumber = "924354114",
                      YoungestChildBirthDate = DateTime.Parse("10-07-1990")
                  }
                  );

            db.SaveChanges();
        }
    }

    
}