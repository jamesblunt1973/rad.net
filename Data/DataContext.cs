using Microsoft.EntityFrameworkCore;
using rad.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Physician> Physicians { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        public DbSet<RawData> RawDatas { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Distribution> Distributions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicine>()
                .HasOne(p => p.Productcer)
                .WithMany(b => b.ProducedMedicines)
                .HasForeignKey(p => p.ProducerId)
                .HasConstraintName("FK_CompanyProducer_Medicine")
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Medicine>()
                .HasOne(p => p.Distributor)
                .WithMany(b => b.DistributedMedicines)
                .HasForeignKey(p => p.DistributorId)
                .HasConstraintName("FK_CompanyDistributor_Medicine")
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Prescription>()
                .HasOne(a => a.RawData)
                .WithOne(b => b.Prescription)
                .HasForeignKey<RawData>(b => b.PrescriptionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
