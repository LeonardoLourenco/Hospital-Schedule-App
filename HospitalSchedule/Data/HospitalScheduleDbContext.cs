using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HospitalSchedule.Models
{
    public class HospitalScheduleDbContext : DbContext
    {
        public HospitalScheduleDbContext (DbContextOptions<HospitalScheduleDbContext> options)
            : base(options)
        {
        }

        public DbSet<HospitalSchedule.Models.Nurse> Nurse { get; set; }
    }
}
