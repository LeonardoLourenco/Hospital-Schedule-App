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
            modelBuilder.Entity<Nurse_Schedule>().HasKey(ns => ns.Nurse_ScheduleID);

            modelBuilder.Entity<Schedule>()
                .HasOne(a => a.OperationBlock)
                .WithOne(b => b.Schedule)
                .HasForeignKey<OperationBlock>(b => b.ScheduleFK);

            modelBuilder.Entity<Nurse_Schedule>()
                .HasKey(s => s.Nurse_ScheduleID);

            // 1 Schedule Tem Varios Nurses
            // O NurseSchedule tem um schedule com muitos ScheduleNurses e Chave Estrangeira NurseID
            modelBuilder.Entity<Nurse_Schedule>()
                           .HasOne(ns => ns.Schedule)
                           .WithMany(n => n.ScheduleNurses)
                           .HasForeignKey(n => n.NurseID);

            //1 Nurse Tem Varios Schedules
            modelBuilder.Entity<Nurse_Schedule>()
                           .HasOne(ns => ns.Nurse)
                           .WithMany(s => s.NurseSchedules)
                           .HasForeignKey(s => s.ScheduleID);

            modelBuilder.Entity<Shift_Schedule>()
                .HasKey(s=> s.Shift_ScheduleID);
        }

        public DbSet<HospitalSchedule.Models.Schedule> Schedule { get; set; }

        public DbSet<HospitalSchedule.Models.Shift_Schedule> Shift_Schedule { get; set; }

        public DbSet<HospitalSchedule.Models.OperationBlock> OperationsBlock { get; set; }

        public DbSet<HospitalSchedule.Models.Nurse_Schedule> Nurses_Schedule { get; set; }
    }
}