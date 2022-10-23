using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class TN408Context : DbContext
    {
        public TN408Context()
        {
        }

        public TN408Context(DbContextOptions<TN408Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AnhSp> AnhSps { get; set; }
        public virtual DbSet<ChiTietDd> ChiTietDds { get; set; }
        public virtual DbSet<DonDat> DonDats { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public virtual DbSet<QuanLyNhapKho> QuanLyNhapKhos { get; set; }
        public virtual DbSet<QuanTri> QuanTris { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TrangThai> TrangThais { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-GQNGQFD7\\SQLEXPRESS;Initial Catalog=TN408;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AnhSp>(entity =>
            {
                entity.HasKey(e => e.MaAnh);

                entity.ToTable("AnhSP");

                entity.Property(e => e.LinkAnh).HasMaxLength(255);

                entity.Property(e => e.MoTa).HasMaxLength(150);

                entity.HasOne(d => d.MaSanPhamNavigation)
                    .WithMany(p => p.AnhSps)
                    .HasForeignKey(d => d.MaSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AnhSP_SanPham");
            });

            modelBuilder.Entity<ChiTietDd>(entity =>
            {
                entity.HasKey(e => new { e.MaSanPham, e.MaDonDat });

                entity.ToTable("ChiTietDD");

                entity.HasOne(d => d.MaDonDatNavigation)
                    .WithMany(p => p.ChiTietDds)
                    .HasForeignKey(d => d.MaDonDat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietDD_DonDat");

                entity.HasOne(d => d.MaSanPhamNavigation)
                    .WithMany(p => p.ChiTietDds)
                    .HasForeignKey(d => d.MaSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChiTietDD_SanPham");
            });

            modelBuilder.Entity<DonDat>(entity =>
            {
                entity.HasKey(e => e.MaDonDat);

                entity.ToTable("DonDat");

                entity.Property(e => e.NgayDatHang).HasMaxLength(50);

                entity.HasOne(d => d.MaNguoiDungNavigation)
                    .WithMany(p => p.DonDats)
                    .HasForeignKey(d => d.MaNguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DonDat_NguoiDung");

                entity.HasOne(d => d.MaTrangThaiNavigation)
                    .WithMany(p => p.DonDats)
                    .HasForeignKey(d => d.MaTrangThai)
                    .HasConstraintName("FK_DonDat_TrangThai");
            });

            modelBuilder.Entity<GioHang>(entity =>
            {
                entity.HasKey(e => new { e.MaTaiKhoan, e.MaSanPham });

                entity.ToTable("GioHang");

                entity.HasOne(d => d.MaSanPhamNavigation)
                    .WithMany(p => p.GioHangs)
                    .HasForeignKey(d => d.MaSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GioHang_SanPham");

                entity.HasOne(d => d.MaTaiKhoanNavigation)
                    .WithMany(p => p.GioHangs)
                    .HasForeignKey(d => d.MaTaiKhoan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GioHang_TaiKhoan");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon);

                entity.ToTable("HoaDon");

                entity.Property(e => e.NgayGiaoHang).HasMaxLength(50);

                entity.HasOne(d => d.MaDonDatNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaDonDat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HoaDon_DonDat");
            });

            modelBuilder.Entity<LoaiSanPham>(entity =>
            {
                entity.HasKey(e => e.MaLoaiSanPham);

                entity.ToTable("LoaiSanPham");

                entity.Property(e => e.TenLoaiSanPham).HasMaxLength(50);
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNguoiDung);

                entity.ToTable("NguoiDung");

                entity.Property(e => e.Avatar).HasMaxLength(50);

                entity.Property(e => e.DiaChi).HasMaxLength(150);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenNguoiDung).HasMaxLength(50);

                entity.HasOne(d => d.MaTaiKhoanNavigation)
                    .WithMany(p => p.NguoiDungs)
                    .HasForeignKey(d => d.MaTaiKhoan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NguoiDung_TaiKhoan");
            });

            modelBuilder.Entity<NhaSanXuat>(entity =>
            {
                entity.HasKey(e => e.MaNhaSanXuat);

                entity.ToTable("NhaSanXuat");

                entity.Property(e => e.TenNhaSanXuat).HasMaxLength(50);
            });

            modelBuilder.Entity<QuanLyNhapKho>(entity =>
            {
                entity.HasKey(e => e.MaNhap);

                entity.ToTable("QuanLyNhapKho");

                entity.Property(e => e.GiaNhap).HasMaxLength(50);

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.HasOne(d => d.MaSanPhamNavigation)
                    .WithMany(p => p.QuanLyNhapKhos)
                    .HasForeignKey(d => d.MaSanPham)
                    .HasConstraintName("FK_QuanLyNhapKho_SanPham");
            });

            modelBuilder.Entity<QuanTri>(entity =>
            {
                entity.HasKey(e => e.MaQuanTri);

                entity.ToTable("QuanTri");

                entity.Property(e => e.Avatar).HasMaxLength(50);

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenQuanTri).HasMaxLength(50);

                entity.HasOne(d => d.MaTaiKhoanNavigation)
                    .WithMany(p => p.QuanTris)
                    .HasForeignKey(d => d.MaTaiKhoan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuanTri_TaiKhoan");
            });

            modelBuilder.Entity<Quyen>(entity =>
            {
                entity.HasKey(e => e.MaQuyen);

                entity.ToTable("Quyen");

                entity.Property(e => e.TenQuyen).HasMaxLength(50);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSanPham);

                entity.ToTable("SanPham");

                entity.Property(e => e.Gia).HasMaxLength(50);

                entity.Property(e => e.Mau).HasMaxLength(50);

                entity.Property(e => e.MoTa).HasMaxLength(50);

                entity.Property(e => e.TenSanPham).HasMaxLength(50);

                entity.Property(e => e.TrangThaiTt).HasColumnName("TrangThaiTT");

                entity.HasOne(d => d.MaLoaiSanPhamNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaLoaiSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SanPham_LoaiSanPham");

                entity.HasOne(d => d.MaNhaSanXuatNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaNhaSanXuat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SanPham_NhaSanXuat");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaTaiKhoan);

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.MatKhau).HasMaxLength(50);

                entity.Property(e => e.TenDangNhap).HasMaxLength(50);

                entity.HasOne(d => d.MaQuyenNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.MaQuyen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaiKhoan_Quyen");
            });

            modelBuilder.Entity<TrangThai>(entity =>
            {
                entity.HasKey(e => e.MaTrangThai);

                entity.ToTable("TrangThai");

                entity.Property(e => e.TenTrangThai).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
