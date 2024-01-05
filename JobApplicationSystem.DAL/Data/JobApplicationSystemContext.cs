using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using JobApplicationSystem.DAL.Models;

namespace JobApplicationSystem.DAL.Data
{
    public partial class JobApplicationSystemContext : DbContext
    {
        public JobApplicationSystemContext()
        {
        }

        public JobApplicationSystemContext(DbContextOptions<JobApplicationSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; } = null!;
        public virtual DbSet<Applicant> Applicants { get; set; } = null!;
        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Employer> Employers { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Resume> Resumes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-OAC2FP3;Database=JobApplicationSystem;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.AdminId);

                entity.ToTable("Administrator");

                entity.HasIndex(e => e.User, "IX_Administrator")
                    .IsUnique();

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserNavigation)
                    .WithOne(p => p.Administrator)
                    .HasForeignKey<Administrator>(d => d.User)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Administrator_AspNetUsers1");
            });

            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.ToTable("Applicant");

                entity.HasIndex(e => e.User, "UQ__Applican__BD20C6F11C4F16BB")
                    .IsUnique();

                entity.Property(e => e.ApplicantId).HasColumnName("ApplicantID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserNavigation)
                    .WithOne(p => p.Applicant)
                    .HasForeignKey<Applicant>(d => d.User)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Applicant_AspNetUsers");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("Application");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Experience)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Resume)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.ApplicantNavigation)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Application_Applicant1");

                entity.HasOne(d => d.JobNavigation)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.Job)
                    .HasConstraintName("FK_Application_Job1");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employer>(entity =>
            {
                entity.ToTable("Employer");

                entity.HasIndex(e => e.User, "UQ__Employer__BD20C6F15E7C9309")
                    .IsUnique();

                entity.Property(e => e.EmployerId).HasColumnName("EmployerID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserNavigation)
                    .WithOne(p => p.Employer)
                    .HasForeignKey<Employer>(d => d.User)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Employer_AspNetUsers");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.Property(e => e.Address).HasMaxLength(450);

                entity.Property(e => e.Company)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyLogo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(450);

                entity.Property(e => e.Education).HasMaxLength(450);

                entity.Property(e => e.Experience).HasMaxLength(450);

                entity.Property(e => e.LastDateToApply).HasColumnType("date");

                entity.Property(e => e.Salary)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmployerNavigation)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.Employer)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Job_Employer1");
            });

            modelBuilder.Entity<Resume>(entity =>
            {
                entity.ToTable("Resume");

                entity.Property(e => e.ResumeId).HasColumnName("ResumeID");

                entity.Property(e => e.ResumeName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
