using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SoftwareProjectManagementSystem.Models
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address");

                entity.Property(e => e.CompanyName).HasColumnName("company name");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("phone")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created by");

                entity.Property(e => e.CreatedFor).HasColumnName("created for");

                entity.Property(e => e.Descrption).HasColumnName("descrption");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start date");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Projects_Users");

                entity.HasOne(d => d.CreatedForNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CreatedFor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Projects_Clients");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Role1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AssignedTo).HasColumnName("assigned to");

                entity.Property(e => e.CreatedBy).HasColumnName("created by");

                entity.Property(e => e.Descrption).HasColumnName("descrption");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.Project).HasColumnName("project");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.TaskAssignedToNavigations)
                    .HasForeignKey(d => d.AssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_Users1");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TaskCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_Users");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_Projects");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date of birth");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("employee id")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("phone")
                    .IsFixedLength(true);

                entity.Property(e => e.Role).HasColumnName("role");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
