using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApplyingUniversityWeb.Models
{
    public partial class ProjectDatabaseContext : DbContext
    {
        public ProjectDatabaseContext()
        {
        }

        public ProjectDatabaseContext(DbContextOptions<ProjectDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<University> University { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.ApplicationId).HasColumnName("applicationId");

                entity.Property(e => e.AppliedDate)
                    .HasColumnName("applied_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UniversityId).HasColumnName("universityId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_UniversityToApplication");

                entity.HasOne(d => d.Username)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserToApplication");
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.Property(e => e.UniversityId).HasColumnName("universityId");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude)
                    .IsRequired()
                    .HasColumnName("latitude")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasColumnName("link")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .IsRequired()
                    .HasColumnName("longitude")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasColumnName("province")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UniversityName)
                    .IsRequired()
                    .HasColumnName("university_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__CB9A1CFFD6DA74D3");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Roll)
                    .IsRequired()
                    .HasColumnName("roll")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
