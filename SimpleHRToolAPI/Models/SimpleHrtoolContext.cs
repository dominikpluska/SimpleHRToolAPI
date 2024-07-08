using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SimpleHRToolAPI.Models.ApprovalRequestModel;
using SimpleHRToolAPI.Models.EmployeeModel;
using SimpleHRToolAPI.Models.LeaveRequestModels;
using SimpleHRToolAPI.Models.ProjectModel;
using SimpleHRToolAPI.Models.UserModel;


namespace SimpleHRToolAPI.Models;

public partial class SimpleHrtoolContext : DbContext
{
    public SimpleHrtoolContext()
    {
    }

    public SimpleHrtoolContext(DbContextOptions<SimpleHrtoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApprovalRequest> ApprovalRequests { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<LoggedSessions> LoggedSessions { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=Venom;Database=SimpleHRTool;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApprovalRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Approval__3214EC27BFF2976A");

            entity.ToTable("ApprovalRequest");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comment).HasMaxLength(2000);
            entity.Property(e => e.Status)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValue("New");

            entity.HasOne(d => d.ApproverNavigation).WithMany(p => p.ApprovalRequests)
                .HasForeignKey(d => d.Approver)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ApprovalR__Appro__3D5E1FD2");

            entity.HasOne(d => d.LeaveRequestNavigation).WithMany(p => p.ApprovalRequests)
                .HasForeignKey(d => d.LeaveRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ApprovalR__Leave__3E52440B");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC27B0106642");

            entity.ToTable("Employee");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FullName).HasMaxLength(600);
            entity.Property(e => e.Photo)
                .HasMaxLength(700)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(600)
                .IsUnicode(false);
            entity.Property(e => e.Subdivision)
                .HasMaxLength(600)
                .IsUnicode(false);

            entity.HasOne(d => d.PeoplePartnerNavigation).WithMany(p => p.InversePeoplePartnerNavigation)
                .HasForeignKey(d => d.PeoplePartner)
                .HasConstraintName("FK__Employee__People__37A5467C");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LeaveReq__3214EC271BB25AAD");

            entity.ToTable("LeaveRequest");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AbsenceReason)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.Comment).HasMaxLength(2000);
            entity.Property(e => e.Status)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.EmployeeNavigation).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.Employee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LeaveRequ__Emplo__3A81B327");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Project__3214EC27C2AB85C1");

            entity.ToTable("Project");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comment).HasMaxLength(2000);
            entity.Property(e => e.ProjectType)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.ProjectManagerNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectManager)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Project__Project__4222D4EF");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC2777AB00AF");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(700)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F2D20773C");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.EmployeeNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Employee)
                .HasConstraintName("FK__Users__Employee__4CA06362");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("FK__Users__Role__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
