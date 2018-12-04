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
        public HospitalScheduleDbContext (DbContextOptions<HospitalScheduleDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tabela Schedule
            modelBuilder.Entity<Schedule>()
                .HasOne(a => a.Nurse)
                .WithMany(S => S.Schedules)
                .HasForeignKey(a => a.NurseId)
                .OnDelete(DeleteBehavior.ClientSetNull);



            //tabela shift schedule operationblock
            modelBuilder.Entity<OperationBlock_Shift>()
                .HasOne(a => a.Shift)
                .WithMany(S => S.Shift_Schedule_OperationBlock)
                .HasForeignKey(a => a.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<OperationBlock_Shift>()
                .HasOne(b => b.Schedule)
                .WithMany(s => s.Shift_Schedule_OperationBlock)
                .HasForeignKey(b => b.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<OperationBlock_Shift>()
                .HasOne(c => c.OperationBlock)
                .WithMany(s => s.Shift_Schedule_OperationBlock)
                .HasForeignKey(c => c.OperationBlockId)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }
        public DbSet<HospitalSchedule.Models.Nurse> Nurse { get; set; }

        public DbSet<HospitalSchedule.Models.Rules> Rules { get; set; }

        public DbSet<HospitalSchedule.Models.Shift> Shift { get; set; }

        public DbSet<HospitalSchedule.Models.Schedule> Schedule { get; set; }

        public DbSet<HospitalSchedule.Models.OperationBlock> OperationBlock { get; set; }

        public DbSet<HospitalSchedule.Models.OperationBlock_Shift> Shift_Schedule_OperationBlock { get; set; }
    }
}
