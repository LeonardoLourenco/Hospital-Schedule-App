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
                .HasForeignKey<OperationBlock>(b => b.ScheduleId);

            // one to many relarionship  Nurse_Schedule
            modelBuilder.Entity<Nurse_Schedule>()
                .HasOne(ns => ns.Nurse)
                .WithMany(n => n.Nurse_Schedules)
                .HasForeignKey(ns => ns.NurseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Nurse_Schedule>()
                .HasOne(ns => ns.Schedule)
                .WithMany(s => s.Nurses_Schedule)
                .HasForeignKey(ns => ns.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // one to many relarionship  Shift_Schedule
            modelBuilder.Entity<Shift_Schedule>()
                .HasOne(Ss => Ss.Shift)
                .WithMany(S => S.Shift_Schedules)
                .HasForeignKey(Ss => Ss.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Shift_Schedule>()
                .HasOne(Ss => Ss.Schedule)
                .WithMany(s => s.Shifts_Schedule)
                .HasForeignKey(Ss => Ss.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }

        public DbSet<HospitalSchedule.Models.Schedule> Schedule { get; set; }

        public DbSet<HospitalSchedule.Models.Shift_Schedule> Shift_Schedule { get; set; }

        public DbSet<HospitalSchedule.Models.OperationBlock> OperationBlock { get; set; }

        public DbSet<HospitalSchedule.Models.Nurse_Schedule> Nurse_Schedule { get; set; }
    }
}