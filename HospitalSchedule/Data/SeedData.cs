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
                SeedNurse_Schedule(db);
                //       SeedOperationBlockModel(db);
                //     SeedSchedule(db);
                //   SeedScheduleCreationModel(db);
                // SeedShift(db);
                //SeedShift_Schedule(db);

            }
        }

        private static void SeedNurse_Schedule(HospitalScheduleDbContext db)
        {
            throw new NotImplementedException();
        }

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
                      CellPhoneNumber = "9212344543",
                      YoungestChildBirthDate = DateTime.Parse("15-05-1977")
                  },
                  new Nurse
                  {
                      Name = "Maria Luiza Ambrosio",
                      Type = 2,
                      Specialties = "Radiologia",
                      Email = "1010381@ipg.pt",
                      CellPhoneNumber = "924354464",
                      YoungestChildBirthDate = DateTime.Parse("15-05-1987")
                  }
                  );
            db.SaveChanges();
        }
    }
}

       
    



    

