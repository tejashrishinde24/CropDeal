using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CropDealBackend.Models;

public partial class CropDealContext : DbContext
{
    public CropDealContext()
    {
    }

    public CropDealContext(DbContextOptions<CropDealContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddOn> AddOns { get; set; }

    public virtual DbSet<AddOnType> AddOnTypes { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<BankDetail> BankDetails { get; set; }

    public virtual DbSet<CropDetail> CropDetails { get; set; }

    public virtual DbSet<CropType> CropTypes { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=CropDeal;TrustServerCertificate=yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddOn>(entity =>
        {
            entity.HasKey(e => e.AddOnId).HasName("PK__AddOn__68270144D44A8C42");

            entity.ToTable("AddOn");

            entity.Property(e => e.AddOnName).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PricePerUnit).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.AddOnType).WithMany(p => p.AddOns)
                .HasForeignKey(d => d.AddOnTypeId)
                .HasConstraintName("FK__AddOn__AddOnType__6383C8BA");

            entity.HasOne(d => d.Admin).WithMany(p => p.AddOns)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__AddOn__AdminId__628FA481");
        });

        modelBuilder.Entity<AddOnType>(entity =>
        {
            entity.HasKey(e => e.AddOnTypeId).HasName("PK__AddOnTyp__C75AAF1F3947BFE3");

            entity.ToTable("AddOnType");

            entity.HasIndex(e => e.AddOnTypeName, "UQ__AddOnTyp__F68257F9236C1EEF").IsUnique();

            entity.Property(e => e.AddOnTypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admins__3214EC07A18B2859");

            entity.HasIndex(e => e.Email, "UQ__Admins__A9D10534B5027751").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BankDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BankDeta__3214EC07ABB6BD70");

            entity.HasIndex(e => e.BankAccountNumber, "UQ__BankDeta__37918BC2C88B309C").IsUnique();

            entity.Property(e => e.AccountHolderName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BankAccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Ifsccode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IFSCCode");

            entity.HasOne(d => d.User).WithMany(p => p.BankDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__BankDetai__UserI__3F466844");
        });

        modelBuilder.Entity<CropDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CropDeta__3214EC0794C0DA32");

            entity.ToTable(tb => tb.HasTrigger("trg_ValidateFarmerId"));

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CropLocation).HasColumnType("text");
            entity.Property(e => e.CropName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.QuantityAvailable).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("Available");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.CropType).WithMany(p => p.CropDetails)
                .HasForeignKey(d => d.CropTypeId)
                .HasConstraintName("FK__CropDetai__CropT__47DBAE45");

            entity.HasOne(d => d.Farmer).WithMany(p => p.CropDetails)
                .HasForeignKey(d => d.FarmerId)
                .HasConstraintName("FK__CropDetai__Farme__46E78A0C");
        });

        modelBuilder.Entity<CropType>(entity =>
        {
            entity.HasKey(e => e.CropTypeId).HasName("PK__CropType__503F4B645F8078CB");

            entity.ToTable("CropType");

            entity.HasIndex(e => e.CropTypeName, "UQ__CropType__302635F0FE9EEF14").IsUnique();

            entity.Property(e => e.CropTypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__0DE60574CCD2ED6C");

            entity.ToTable("Invoice");

            entity.Property(e => e.InvoiceId).HasColumnName("Invoice_Id");
            entity.Property(e => e.DealerId).HasColumnName("Dealer_Id");
            entity.Property(e => e.FarmerId).HasColumnName("Farmer_Id");
            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PricePerKg).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalAmount)
                .HasComputedColumnSql("([Quantity]*[PricePerKg])", true)
                .HasColumnType("decimal(21, 4)");
            entity.Property(e => e.TransactionStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.AddOn).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.AddOnId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Invoice_AddOn");

            entity.HasOne(d => d.Crop).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CropId)
                .HasConstraintName("FK__Invoice__CropId__534D60F1");

            entity.HasOne(d => d.Dealer).WithMany(p => p.InvoiceDealers)
                .HasForeignKey(d => d.DealerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Dealer___5165187F");

            entity.HasOne(d => d.Farmer).WithMany(p => p.InvoiceFarmers)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Farmer___52593CB8");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07D8ACBCD7");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Message).HasMaxLength(500);

            entity.HasOne(d => d.Crop).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.CropId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_Crop");

            entity.HasOne(d => d.Dealer).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.DealerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_Dealer");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrip__3214EC07D52F97AC");

            entity.ToTable(tb => tb.HasTrigger("trg_ValidateDealerId"));

            entity.Property(e => e.IsNotificationEnabled).HasDefaultValue(true);
            entity.Property(e => e.SubscriptionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Crop).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.CropId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subscript__CropI__4BAC3F29");

            entity.HasOne(d => d.Dealer).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.DealerId)
                .HasConstraintName("FK__Subscript__Deale__4AB81AF0");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__9A8D5605AE33888D");

            entity.ToTable(tb => tb.HasTrigger("trg_ValidateFarmerDealerId"));

            entity.Property(e => e.TransactionId).HasColumnName("Transaction_Id");
            entity.Property(e => e.DealerId).HasColumnName("Dealer_Id");
            entity.Property(e => e.FarmerId).HasColumnName("Farmer_Id");
            entity.Property(e => e.InvoiceId).HasColumnName("Invoice_Id");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionMode)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.DealerBankAcc).WithMany(p => p.TransactionDealerBankAccs)
                .HasForeignKey(d => d.DealerBankAccId)
                .HasConstraintName("fk_dealer_bank_acc");

            entity.HasOne(d => d.Dealer).WithMany(p => p.TransactionDealers)
                .HasForeignKey(d => d.DealerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Deale__59063A47");

            entity.HasOne(d => d.FarmerBankAcc).WithMany(p => p.TransactionFarmerBankAccs)
                .HasForeignKey(d => d.FarmerBankAccId)
                .HasConstraintName("fk_farmer_bank_acc");

            entity.HasOne(d => d.Farmer).WithMany(p => p.TransactionFarmers)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Farme__59FA5E80");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Transactions_Invoice");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserDeta__3214EC279F09C14D");

            entity.HasIndex(e => e.LoginId, "UQ__UserDeta__4DDA281905C5A20F").IsUnique();

            entity.HasIndex(e => e.EmailId, "UQ__UserDeta__7ED91ACE84C7A731").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ__UserDeta__85FB4E385717869D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.EmailId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.LoginId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
