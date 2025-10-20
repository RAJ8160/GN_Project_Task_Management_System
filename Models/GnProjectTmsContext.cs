using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GN_Project_Task_Management_System.Models;

public partial class GnProjectTmsContext : DbContext
{
    public GnProjectTmsContext()
    {
    }

    public GnProjectTmsContext(DbContextOptions<GnProjectTmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<IssueType> IssueTypes { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=RAJ\\SQLEXPRESS;Database=GN_Project_TMS;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Activity__5E5499A8F53D00A4");

            entity.ToTable("ActivityLog");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.ActionTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ActionType).HasMaxLength(50);
            entity.Property(e => e.ActiveActivityLog).HasDefaultValue(true);
            entity.Property(e => e.IssueId).HasColumnName("IssueID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Issue).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("FK__ActivityL__Issue__2CF2ADDF");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ActivityL__UserI__2DE6D218");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFAAF95E6436");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.ActiveComment).HasDefaultValue(true);
            entity.Property(e => e.CommentText).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IssueId).HasColumnName("IssueID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Issue).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("FK__Comments__IssueI__2739D489");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comments__UserID__282DF8C2");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.IssueId).HasName("PK__Issues__6C861624D8C5EAA1");

            entity.Property(e => e.IssueId).HasColumnName("IssueID");
            entity.Property(e => e.ActiveIssue).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Priority).HasMaxLength(10);
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.SprintId).HasColumnName("SprintID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Issues)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK__Issues__Assigned__208CD6FA");

            entity.HasOne(d => d.Project).WithMany(p => p.Issues)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Issues__ProjectI__1DB06A4F");

            entity.HasOne(d => d.Sprint).WithMany(p => p.Issues)
                .HasForeignKey(d => d.SprintId)
                .HasConstraintName("FK__Issues__SprintID__1EA48E88");

            entity.HasOne(d => d.Type).WithMany(p => p.Issues)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Issues__TypeID__1F98B2C1");
        });

        modelBuilder.Entity<IssueType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__IssueTyp__516F039566CE54DD");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.ActiveType).HasDefaultValue(true);
            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABED0DF80634B");

            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.ActiveProject).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ProjectName).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Projects__Create__114A936A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A8EA805E3");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61609A19423E").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.ActiveRole).HasDefaultValue(true);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasKey(e => e.SprintId).HasName("PK__Sprints__29F16AE0EA6E3E4F");

            entity.Property(e => e.SprintId).HasColumnName("SprintID");
            entity.Property(e => e.ActiveSprint).HasDefaultValue(true);
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.SprintName).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Project).WithMany(p => p.Sprints)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Sprints__Project__160F4887");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC6F88FF54");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053474FE4DAB").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456E5904543").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.ActiveUser).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(150);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A55A017CB47");

            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.ActiveUserRole).HasDefaultValue(true);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRoles__RoleI__0D7A0286");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRoles__UserI__0C85DE4D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
