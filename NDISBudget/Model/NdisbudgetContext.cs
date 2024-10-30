using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NDISBudget.Model;

public partial class NdisbudgetContext : DbContext
{
    public NdisbudgetContext()
    {
    }

    public NdisbudgetContext(DbContextOptions<NdisbudgetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<ClientBudget> ClientBudgets { get; set; }

    public virtual DbSet<ClientSupportItem> ClientSupportItems { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyType> CompanyTypes { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<SupportItem> SupportItems { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<WeekDay> WeekDays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=gmiaustraliadb.c93jjclziv58.ap-southeast-2.rds.amazonaws.com;Database=NDISBudget;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK_Admin");

            entity.ToTable("Admin");

            entity.Property(e => e.Username)
                .HasMaxLength(50);

            entity.Property(e => e.Password)
                .HasMaxLength(50);
                
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasMaxLength(50);
            entity.Property(e => e.EmailId).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<ClientBudget>(entity =>
        {
            entity.HasKey(e => e.ClientBudgetId).HasName("PK_Budget");

            entity.ToTable("ClientBudget");

            entity.Property(e => e.CompanyId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ProposedBudget).HasColumnType("money");
        });

        modelBuilder.Entity<ClientSupportItem>(entity =>
        {
            entity.ToTable("ClientSupportItem");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasMaxLength(50);
            entity.Property(e => e.ItemBudget).HasColumnType("money");
            entity.Property(e => e.ShiftEndTime).HasMaxLength(20);
            entity.Property(e => e.ShiftStartTime).HasMaxLength(20);
            entity.Property(e => e.StartDate).HasMaxLength(50);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_Company");

            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.ContactNumber).HasMaxLength(15);
            entity.Property(e => e.CreatedDate)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.EmailId).HasMaxLength(150);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<CompanyType>(entity =>
        {
            entity.HasKey(e => e.CompanyTypeId).HasName("PK_User");

            entity.ToTable("CompanyType");

            entity.Property(e => e.CompanyType1)
                .HasMaxLength(50)
                .HasColumnName("CompanyType");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.GenderId).ValueGeneratedNever();
            entity.Property(e => e.GenderType).HasMaxLength(50);
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.HasKey(e => e.HolidayId).HasName("PK_Holyday");

            entity.ToTable("Holiday");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.ToTable("Provider");

            entity.Property(e => e.FundingAmount).HasColumnType("money");
            entity.Property(e => e.ProviderName).HasMaxLength(50);
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.ToTable("Region");

            entity.Property(e => e.Region1)
                .HasMaxLength(50)
                .HasColumnName("Region");
            entity.Property(e => e.State).HasMaxLength(150);
        });

        modelBuilder.Entity<SupportItem>(entity =>
        {
            entity.ToTable("SupportItem");

            entity.Property(e => e.ItemPrice).HasColumnType("money");
            entity.Property(e => e.SupportItemName).HasMaxLength(100);
            entity.Property(e => e.Usage).HasMaxLength(50);
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.ToTable("TimeSlot");

            entity.Property(e => e.TimeSlot1)
                .HasMaxLength(10)
                .HasColumnName("TimeSlot");
        });

        modelBuilder.Entity<WeekDay>(entity =>
        {
            entity.ToTable("WeekDay");

            entity.Property(e => e.WeekDay1)
                .HasMaxLength(50)
                .HasColumnName("WeekDay");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
