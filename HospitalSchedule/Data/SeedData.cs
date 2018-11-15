using HospitalSchedule.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Data
{
    public class SeedData
    {
        internal static void Populate(IServiceProvider applicationServices)
        {

            using (var serviceScope = applicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<HospitalScheduleDbContext>();
                if (db.Nurses.Any()) return;

                SeedNurses(db);
                //SeedNurses_Schedules(db);
                //      SeedOperationBlockModel(db);
                //     SeedSchedule(db);
                //   SeedScheduleCreationModel(db);
                SeedShifts(db);
                SeedShift_Schedule(db);

            }
        }

        private static void SeedShift_Schedule(HospitalScheduleDbContext db)
        {
            if (db.Shift_Schedule.Any()) return;
            db.Shift_Schedule.AddRange(
                  new Shift_Schedule
                  {
                     ShiftDate = DateTime.Parse("15-05-2018")
                     
                  });

            db.SaveChanges();
        }
    

        private static void SeedShifts(HospitalScheduleDbContext db)
        {
            if (db.Shift.Any()) return;
            db.Shift.AddRange(
                  new Shift
                  {
                      Request = "Pedro Miguel Adelino",
                      Accept = "Ana Leopoldina",
                      ShiftName = "T1",
                      StartingHour = new DateTime(2018, 5, 1, 8, 30, 00),
                      FinishingHour = new DateTime(2018, 5, 1, 16, 30, 00)
                  },
                  new Shift
                  {
                      Request = "Mario André",
                      Accept = "Raquel Patricio",
                      ShiftName = "T1",
                      StartingHour = new DateTime(2018, 5, 1, 16, 30, 00),
                      FinishingHour = new DateTime(2018, 5, 1, 00, 30, 00)
                  },
                   new Shift
                   {
                       Request = "Amandio Miguel Adelino",
                       Accept = "Ana Leopoldina",
                       ShiftName = "T2",
                       StartingHour = new DateTime(2018, 11, 1, 8, 30, 00),
                       FinishingHour = new DateTime(2018, 5, 1, 16, 30, 00)
                   },
                  new Shift
                  {
                      Request = "Miguel Sebastião",
                      Accept = "Raquel Patricio",
                      ShiftName = "T2",
                      StartingHour = new DateTime(2018, 5, 1, 16, 30, 00),
                      FinishingHour = new DateTime(2018, 5, 1, 00, 30, 00)
                  },
                      new Shift
                  {
                      Request = "Amandio Miguel Adelino",
                      Accept = "Ana Leopoldina",
                      ShiftName = "T2",
                      StartingHour = new DateTime (2018, 5, 1, 00, 30, 00),
                      FinishingHour = new DateTime(2018, 5, 1, 08, 30, 00)

                  });

            db.SaveChanges();
        }

        //    private static void SeedNurses_Schedules(HospitalScheduleDbContext db)
        //{
        //    throw new NotImplementedException();
        //}

        private static void SeedNurses(HospitalScheduleDbContext db)
        {

            if (db.Nurses.Any()) return;
            db.Nurses.AddRange(

                  new Nurse
                  {
                      Name = "Fabio Miguel Ambrosio",
                      Type = 2,
                      Specialties = "Cardiologia",
                      Email = "1011881@ipg.pt",
                      CellPhoneNumber = "921234543",
                      YoungestChildBirthDate = DateTime.Parse("15-05-1987")
                  },
                  new Nurse
                  {
                      Name = "Maria Luiza Ambrosio",
                      Type = 3,
                      Specialties = "Radiologia",
                      Email = "1010381@ipg.pt",
                      CellPhoneNumber = "924354464",
                      YoungestChildBirthDate = DateTime.Parse("15-05-1987")
                  },
                  new Nurse
                    {
                        Name = "Diana Miguel Rapozo",
                        Type = 2,
                        Specialties = "Cardiologia",
                        Email = "1111811@ipg.pt",
                        CellPhoneNumber = "921234543",
                        YoungestChildBirthDate = DateTime.Parse("11-05-1981")
                    },
                  new Nurse
                  {
                      Name = "Mara Andrade Gil",
                      Type = 3,
                      Specialties = "Radiologia",
                      Email = "101121@ipg.pt",
                      CellPhoneNumber = "924354114",
                      YoungestChildBirthDate = DateTime.Parse("10-07-1990")                                                  
                  }
                  );
            
            db.SaveChanges();
        }
    }
}

       
    



    

