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

        internal static void Populate(IServiceProvider applicationServices)
        {

            using (var serviceScope = applicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<HospitalScheduleDbContext>();


                SeedNurses(db);
                SeedSchedule(db);
                SeedOperationBlock(db);
                SeedShifts(db);
                SeedShifts_Schedule_OperationBlocks(db);

            }
        }

        private static void SeedShifts_Schedule_OperationBlocks(HospitalScheduleDbContext db)
        {

            //if (db.Shift_Schedule_OperationBlock.Any()) return;

            Schedule schedule = db.Schedule.SingleOrDefault(s => s.Nurse.Name == "Diana Miguel Rapozo");
            //Shift shift = db.Shift.SingleOrDefault(s => s.ShiftId == 2);
            //OperationBlock operationBlock = db.OperationBlock.SingleOrDefault(o => o.OperationBlockId == 3);

            db.Shift_Schedule_OperationBlock.Add(
                  new Shift_Schedule_OperationBlock
                  {
                      ShiftDate = new DateTime(2018, 5, 1, 16, 30, 00),
                      ScheduleId = schedule.ScheduleId,
                      ShiftId = 1,
                      OperationBlockId =1
                  });

            db.SaveChanges();
        }


        private static void SeedShifts(HospitalScheduleDbContext db)
        {
            if (db.Shift.Any()) return;
            db.Shift.AddRange(
                  new Shift
                  {
                       
                     ShiftName = "T1",
                     StartingHour = new DateTime(2018, 5, 1, 8, 30, 00),
                     FinishingHour = new DateTime(2018, 5, 1, 16, 30, 00)
                  },
                  new Shift
                  {
                         
                      ShiftName = "T1",
                      StartingHour = new DateTime(2018, 5, 1, 16, 30, 00),
                      FinishingHour = new DateTime(2018, 5, 1, 00, 30, 00)
                  },
                   new Shift
                   {
                      
                       ShiftName = "T2",
                       StartingHour = new DateTime(2018, 11, 1, 8, 30, 00),
                       FinishingHour = new DateTime(2018, 5, 1, 16, 30, 00)
                   },
                  new Shift
                  {
                          
                      ShiftName = "T2",
                      StartingHour = new DateTime(2018, 5, 1, 16, 30, 00),
                      FinishingHour = new DateTime(2018, 5, 1, 00, 30, 00)
                  },
                      new Shift
                      {
                             
                          ShiftName = "Diana T2",
                          StartingHour = new DateTime(2018, 5, 1, 00, 30, 00),
                          FinishingHour = new DateTime(2018, 5, 1, 08, 30, 00)

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
                      TypeOfShift = "Manha",
                     
                  },
                  new OperationBlock
                  {
                      BlockName = "bloco C",
                      TypeOfShift = "Manha"
                     
                  });
            db.SaveChanges();
        }
    

        private static void SeedSchedule(HospitalScheduleDbContext db)
        {
            if (db.Schedule.Any()) return;
            db.Schedule.AddRange(
                  new Schedule
                  {
                      Date = new DateTime(2018, 5, 1, 8, 30, 00),
                      NurseId = 3,


                  },
                  new Schedule
                  {
                      Date = new DateTime(2018, 5, 1, 8, 30, 00),
                      NurseId = 2


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
                          CCBI = "22345576",
                          Type = 2,
                          Specialties = "Cardiologia",
                          Email = "1011881@ipg.pt",
                          CellPhoneNumber = "921234543",
                          YoungestChildBirthDate = DateTime.Parse("15-05-1987")
                      },
                      new Nurse
                      {
                          Name = "Maria Luiza Ambrosio",
                          BirthDate = new DateTime(1988, 5, 1),
                          CCBI = "22345576",
                          Type = 3,
                          Specialties = "Radiologia",
                          Email = "1010381@ipg.pt",
                          CellPhoneNumber = "924354464",
                          YoungestChildBirthDate = DateTime.Parse("15-05-1987")
                      },
                      new Nurse
                      {
                          Name = "Diana Miguel Rapozo",
                          BirthDate = new DateTime(1988, 5, 1),
                          CCBI = "42344576",
                          Type = 2,
                          Specialties = "Cardiologia",
                          Email = "1111811@ipg.pt",
                          CellPhoneNumber = "921234543",
                          YoungestChildBirthDate = DateTime.Parse("11-05-1981")
                      },
                      new Nurse
                      {
                          Name = "Mara Andrade Gil",
                          BirthDate = new DateTime(1988, 5, 1),
                          CCBI = "228574576",
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
