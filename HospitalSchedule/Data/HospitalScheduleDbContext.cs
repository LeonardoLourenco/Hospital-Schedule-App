using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HospitalSchedule.Models;

namespace HospitalSchedule.Models
{
    public class HospitalScheduleDbContext : DbContext
    {
        public HospitalScheduleDbContext(DbContextOptions<HospitalScheduleDbContext> options)
            : base(options)
        {
        }

        public DbSet<HospitalSchedule.Models.Nurse> Nurse { get; set; }

        public DbSet<HospitalSchedule.Models.Shift> Shift { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .HasOne(a => a.OperationBlock)
                .WithOne(b => b.Schedule)
                .HasForeignKey<OperationBlock>(b => b.ScheduleFK);
        }

        public DbSet<HospitalSchedule.Models.Schedule> Schedule { get; set; }

        public DbSet<HospitalSchedule.Models.Shift_Schedule> Shift_Schedule { get; set; }
    }
}