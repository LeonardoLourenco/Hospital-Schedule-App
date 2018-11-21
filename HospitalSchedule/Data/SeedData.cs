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
                if (db.Nurse.Any()) return;

                SeedNurses(db);
                //SeedNurses_Schedules(db);
                //      SeedOperationBlockModel(db);
                SeedSchedule(db);
                //   SeedScheduleCreationModel(db);
                SeedShifts(db);
                //SeedShift_Schedule(db);

            }
        }

        private static void SeedSchedule(HospitalScheduleDbContext db)
        {
            if (db.Schedule.Any()) return;
            db.Schedule.AddRange(
                  new Schedule
                  {

                      CreationDate = new DateTime(2018, 5, 1, 8, 30, 00),
                      FinishedDate = new DateTime(2018, 5, 2, 8, 30, 00),
                      AtiveSchedule = true
                  },
                  new Schedule
                  {


                      CreationDate = new DateTime(2018, 5, 3, 00, 30, 00),
                      FinishedDate = new DateTime(2018, 5, 4, 08, 30, 00),
                      AtiveSchedule = true
                  });

            db.SaveChanges();
        }
        

        //private static void seedshift_schedule(hospitalscheduledbcontext db)
        //{
        //    if (db.shift_schedule.any()) return;
        //    db.shift_schedule.addrange(
        //          new shift_schedule
        //          {
        //              shiftdate = datetime.parse("15-05-2018")

        //          });

        //    db.savechanges();
        //}


        private static void SeedShifts(HospitalScheduleDbContext db)
        {
            if (db.Shift.Any()) return;
            db.Shift.AddRange(
                  new Shift
                  {
                      // Request = "Pedro Miguel Adelino",
                      // Accept = "Ana Leopoldina",
                      ShiftName = "T1",
                      StartingHour = new DateTime(2018, 5, 1, 8, 30, 00),
                      FinishingHour = new DateTime(2018, 5, 1, 16, 30, 00)
                  },
                  new Shift
                  {
                      //Request = "Mario André",
                      //  Accept = "Raquel Patricio",
                      ShiftName = "T1",
                      StartingHour = new DateTime(2018, 5, 1, 16, 30, 00),
                      FinishingHour = new DateTime(2018, 5, 1, 00, 30, 00)
                  },
                   new Shift
                   {
                       // /Request = "Amandio Miguel Adelino",
                       // Accept = "Ana Leopoldina",
                       ShiftName = "T2",
                       StartingHour = new DateTime(2018, 11, 1, 8, 30, 00),
                       FinishingHour = new DateTime(2018, 5, 1, 16, 30, 00)
                   },
                  new Shift
                  {
                      // Request = "Miguel Sebastião",
                      // Accept = "Raquel Patricio",
                      ShiftName = "T2",
                      StartingHour = new DateTime(2018, 5, 1, 16, 30, 00),
                      FinishingHour = new DateTime(2018, 5, 1, 00, 30, 00)
                  },
                      new Shift
                      {
                          //Request = "Amandio Miguel Adelino",
                          // Accept = "Ana Leopoldina",
                          ShiftName = "T2",
                          StartingHour = new DateTime(2018, 5, 1, 00, 30, 00),
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

            if (db.Nurse.Any()) return;
            db.Nurse.AddRange(

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

       
    



    

