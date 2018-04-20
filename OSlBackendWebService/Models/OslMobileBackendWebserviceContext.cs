using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OSlBackendWebService.Models
{
    public partial class OslMobileBackendWebserviceContext : DbContext
    {
        public virtual DbSet<Checkings> Checkings { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<EmployeesLogs> EmployeesLogs { get; set; }
        public virtual DbSet<Stations> Stations { get; set; }
        public virtual DbSet<Supervisors> Supervisors { get; set; }
        public OslMobileBackendWebserviceContext(DbContextOptions<OslMobileBackendWebserviceContext>options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
             //To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=TS-PC\SQLEXPRESS;Initial Catalog=OslMobileBackendWebservice;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Checkings>(entity =>
            {
                entity.HasKey(e => e.TranszactionId);

                entity.Property(e => e.TranszactionId).HasColumnName("TranszactionID");

                entity.Property(e => e.EmpId)
                    .HasColumnName("EmpID")
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.Station).HasColumnType("nchar(10)");

                entity.Property(e => e.SupId).HasColumnName("SupID");

                entity.HasOne(d => d.Sup)
                    .WithMany(p => p.Checkings)
                    .HasForeignKey(d => d.SupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Checkings_Supervisors");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.Property(e => e.EmpId)
                    .HasColumnName("EmpID")
                    .ValueGeneratedNever();

                entity.Property(e => e.StationId).HasColumnName("StationID");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Employees_Stations");

                entity.HasOne(d => d.StationNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StationId)
                    .HasConstraintName("FK_Employees_Supervisors");
            });

            modelBuilder.Entity<EmployeesLogs>(entity =>
            {
                entity.HasKey(e => e.TranszactionId);

                entity.Property(e => e.TranszactionId).HasColumnName("TranszactionID");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.Lit)
                    .HasColumnName("LIT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Lot)
                    .HasColumnName("LOT")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.EmployeesLogs)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK_EmployeesLogs_Employees");
            });

            modelBuilder.Entity<Stations>(entity =>
            {
                entity.HasKey(e => e.Stid);

                entity.Property(e => e.Stid)
                    .HasColumnName("STID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.StationName).HasMaxLength(50);

                entity.Property(e => e.SupId).HasColumnName("SupID");

                entity.HasOne(d => d.Sup)
                    .WithMany(p => p.Stations)
                    .HasForeignKey(d => d.SupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Stations_Supervisors");
            });

            modelBuilder.Entity<Supervisors>(entity =>
            {
                entity.HasKey(e => e.SupId);

                entity.Property(e => e.SupId)
                    .HasColumnName("SupID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.Phone).HasColumnType("nchar(10)");
            });
        }
    }
}
