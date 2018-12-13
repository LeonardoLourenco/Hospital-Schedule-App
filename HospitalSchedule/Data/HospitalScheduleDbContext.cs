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

        protected override void OnModelCreating(ModelBuilder modelBuilder)  //
        {
            //Composed primary key
            //modelBuilder.Entity<OperationBlock_Shifts>().HasKey(o => new { o.OperationBlockId, o.ShiftId }); //indica a chave

            // one to many relarionship OperationBlock_Shifts
            modelBuilder.Entity<OperationBlock_Shifts>()  //indica como é feita a relação
                .HasOne(bc => bc.OperationBlock)
                .WithMany(b => b.OperationBlock_Shifts)
                .HasForeignKey(bc => bc.OperationBlockId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<OperationBlock_Shifts>()
                .HasOne(bc => bc.Shift)
                .WithMany(c => c.OperationBlock_Shifts)
                .HasForeignKey(bc => bc.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            // one to many relarionship Schedule
            modelBuilder.Entity<Schedule>()
                .HasOne(bc => bc.Nurse)
                .WithMany(c => c.Schedules)
                .HasForeignKey(bc => bc.NurseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Schedule>()
                .HasOne(bc => bc.OperationBlock_Shifts)
                .WithMany(c => c.Schedules)
                .HasForeignKey(bc => bc.OperationBlock_ShiftsId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            // one to many relarionship Nurse
            modelBuilder.Entity<Nurse>()
                .HasOne(bc => bc.Specialty)
                .WithMany(c => c.Nurses)
                .HasForeignKey(bc => bc.SpecialtyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<HospitalSchedule.Models.Nurse> Nurse { get; set; }

        public DbSet<HospitalSchedule.Models.OperationBlock> OperationBlock { get; set; }

        public DbSet<HospitalSchedule.Models.Shift> Shift { get; set; }

        public DbSet<HospitalSchedule.Models.OperationBlock_Shifts> OperationBlock_Shifts { get; set; }

        public DbSet<HospitalSchedule.Models.Rules> Rules { get; set; }

        public DbSet<HospitalSchedule.Models.Specialty> Specialty { get; set; }

        public DbSet<HospitalSchedule.Models.Schedule> Schedule { get; set; }
    }
    }
