using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Models
{
    public partial class sql_NCKHContext : DbContext
    {
        public sql_NCKHContext()
        {
        }

        public sql_NCKHContext(DbContextOptions<sql_NCKHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tblboiduong> Tblboiduongs { get; set; }
        public virtual DbSet<Tblchucvu> Tblchucvus { get; set; }
        public virtual DbSet<Tblcongtac> Tblcongtacs { get; set; }
        public virtual DbSet<Tbldetai> Tbldetais { get; set; }
        public virtual DbSet<Tbldonvi> Tbldonvis { get; set; }
        public virtual DbSet<Tblhoatdongnckh> Tblhoatdongnckhs { get; set; }
        public virtual DbSet<Tblhoso> Tblhosos { get; set; }
        public virtual DbSet<Tbllinhvuc> Tbllinhvucs { get; set; }
        public virtual DbSet<Tblloaihoatdong> Tblloaihoatdongs { get; set; }
        public virtual DbSet<Tblloainghiencuu> Tblloainghiencuus { get; set; }
        public virtual DbSet<Tblloainhiemvu> Tblloainhiemvus { get; set; }
        public virtual DbSet<Tblloaitt> Tblloaitts { get; set; }
        public virtual DbSet<Tbllylich> Tbllyliches { get; set; }
        public virtual DbSet<Tblnhanvien> Tblnhanviens { get; set; }
        public virtual DbSet<Tblnhomtg> Tblnhomtgs { get; set; }
        public virtual DbSet<Tblphanhoi> Tblphanhois { get; set; }
        public virtual DbSet<Tblphongban> Tblphongbans { get; set; }
        public virtual DbSet<Tblsohuu> Tblsohuus { get; set; }
        public virtual DbSet<Tblsohuudetai> Tblsohuudetais { get; set; }
        public virtual DbSet<Tbltintuc> Tbltintucs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ADMIN;Database=sql_NCKH;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<Tblboiduong>(entity =>
            {
                entity.ToTable("tblboiduong");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Idnv).HasColumnName("idnv");

                entity.Property(e => e.Ngaybd)
                    .HasColumnType("date")
                    .HasColumnName("ngaybd");

                entity.Property(e => e.Ngaykt)
                    .HasColumnType("date")
                    .HasColumnName("ngaykt");

                entity.Property(e => e.Noibd).HasColumnName("noibd");

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.HasOne(d => d.IdnvNavigation)
                    .WithMany(p => p.Tblboiduongs)
                    .HasForeignKey(d => d.Idnv)
                    .HasConstraintName("FK_tblboiduong_tblnhanvien");
            });

            modelBuilder.Entity<Tblchucvu>(entity =>
            {
                entity.ToTable("tblchucvu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dieukien).HasColumnName("dieukien");

                entity.Property(e => e.Dinhmuc).HasColumnName("dinhmuc");

                entity.Property(e => e.Ghichu).HasColumnName("ghichu");

                entity.Property(e => e.Tenchucvu).HasColumnName("tenchucvu");
            });

            modelBuilder.Entity<Tblcongtac>(entity =>
            {
                entity.ToTable("tblcongtac");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Chucdanh).HasColumnName("chucdanh");

                entity.Property(e => e.Chucvu).HasColumnName("chucvu");

                entity.Property(e => e.Idnv).HasColumnName("idnv");

                entity.Property(e => e.Ngaybad)
                    .HasColumnType("date")
                    .HasColumnName("ngaybad");

                entity.Property(e => e.Ngaykt)
                    .HasColumnType("date")
                    .HasColumnName("ngaykt");

                entity.Property(e => e.Noict).HasColumnName("noict");

                entity.HasOne(d => d.IdnvNavigation)
                    .WithMany(p => p.Tblcongtacs)
                    .HasForeignKey(d => d.Idnv)
                    .HasConstraintName("FK_tblcongtac_tblnhanvien");
            });

            modelBuilder.Entity<Tbldetai>(entity =>
            {
                entity.ToTable("tbldetai");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ghichu).HasColumnName("ghichu");

                entity.Property(e => e.Idhdnckh).HasColumnName("idhdnckh");

                entity.Property(e => e.Idlinhvuc).HasColumnName("idlinhvuc");

                entity.Property(e => e.Idloainv).HasColumnName("idloainv");

                entity.Property(e => e.Idlsp).HasColumnName("idlsp");

                entity.Property(e => e.Minhchung).HasColumnName("minhchung");

                entity.Property(e => e.Namsx).HasColumnName("namsx");

                entity.Property(e => e.So).HasColumnName("so");

                entity.Property(e => e.Sohieu).HasColumnName("sohieu");

                entity.Property(e => e.Soif)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("soif");

                entity.Property(e => e.Tap).HasColumnName("tap");

                entity.Property(e => e.Tentc)
                    .HasMaxLength(10)
                    .HasColumnName("tentc")
                    .IsFixedLength(true);

                entity.Property(e => e.Tinhtrang).HasColumnName("tinhtrang");

                entity.Property(e => e.Trang).HasColumnName("trang");

                entity.Property(e => e.Uytin).HasColumnName("uytin");

                entity.HasOne(d => d.IdhdnckhNavigation)
                    .WithMany(p => p.Tbldetais)
                    .HasForeignKey(d => d.Idhdnckh)
                    .HasConstraintName("FK_tbldetai_tblhoatdongnckh");

                entity.HasOne(d => d.IdlinhvucNavigation)
                    .WithMany(p => p.Tbldetais)
                    .HasForeignKey(d => d.Idlinhvuc)
                    .HasConstraintName("FK_tbldetai_tbllinhvuc1");

                entity.HasOne(d => d.IdloainvNavigation)
                    .WithMany(p => p.Tbldetais)
                    .HasForeignKey(d => d.Idloainv)
                    .HasConstraintName("FK_tbldetai_tblloainhiemvu");

                entity.HasOne(d => d.IdlspNavigation)
                    .WithMany(p => p.Tbldetais)
                    .HasForeignKey(d => d.Idlsp)
                    .HasConstraintName("FK_tbldetai_tblloainghiencuu");
            });

            modelBuilder.Entity<Tbldonvi>(entity =>
            {
                entity.ToTable("tbldonvi");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ghichu).HasColumnName("ghichu");

                entity.Property(e => e.Tendv)
                    .HasMaxLength(50)
                    .HasColumnName("tendv");

                entity.Property(e => e.Tyle).HasColumnName("tyle");
            });

            modelBuilder.Entity<Tblhoatdongnckh>(entity =>
            {
                entity.ToTable("tblhoatdongnckh");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dinhmuc).HasColumnName("dinhmuc");

                entity.Property(e => e.Dmhtkinhphi).HasColumnName("dmhtkinhphi");

                entity.Property(e => e.Ghichu).HasColumnName("ghichu");

                entity.Property(e => e.Idloaihd).HasColumnName("idloaihd");

                entity.Property(e => e.Tenhdnckh).HasColumnName("tenhdnckh");

                entity.HasOne(d => d.IdloaihdNavigation)
                    .WithMany(p => p.Tblhoatdongnckhs)
                    .HasForeignKey(d => d.Idloaihd)
                    .HasConstraintName("FK_tblhoatdongnckh_tblloaihoatdong");
            });

            modelBuilder.Entity<Tblhoso>(entity =>
            {
                entity.ToTable("tblhoso");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Iddetai).HasColumnName("iddetai");

                entity.Property(e => e.Minhchung)
                    .IsUnicode(false)
                    .HasColumnName("minhchung");

                entity.Property(e => e.Ngay)
                    .HasColumnType("date")
                    .HasColumnName("ngay");

                entity.Property(e => e.Ten)
                    .HasMaxLength(255)
                    .HasColumnName("ten");

                entity.HasOne(d => d.IddetaiNavigation)
                    .WithMany(p => p.Tblhosos)
                    .HasForeignKey(d => d.Iddetai)
                    .HasConstraintName("FK_tblhoso_tbldetai");
            });

            modelBuilder.Entity<Tbllinhvuc>(entity =>
            {
                entity.ToTable("tbllinhvuc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tenlinhvuc).HasColumnName("tenlinhvuc");
            });

            modelBuilder.Entity<Tblloaihoatdong>(entity =>
            {
                entity.ToTable("tblloaihoatdong");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ghichu).HasColumnName("ghichu");

                entity.Property(e => e.Tenloaihd).HasColumnName("tenloaihd");
            });

            modelBuilder.Entity<Tblloainghiencuu>(entity =>
            {
                entity.ToTable("tblloainghiencuu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tenloai).HasColumnName("tenloai");
            });

            modelBuilder.Entity<Tblloainhiemvu>(entity =>
            {
                entity.ToTable("tblloainhiemvu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ghichu).HasColumnName("ghichu");

                entity.Property(e => e.Tenloainv).HasColumnName("tenloainv");
            });

            modelBuilder.Entity<Tblloaitt>(entity =>
            {
                entity.ToTable("tblloaitt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tenloaitt)
                    .HasMaxLength(255)
                    .HasColumnName("tenloaitt");
            });

            modelBuilder.Entity<Tbllylich>(entity =>
            {
                entity.ToTable("tbllylich");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bangdaihoc).HasColumnName("bangdaihoc");

                entity.Property(e => e.Bangthacsi).HasColumnName("bangthacsi");

                entity.Property(e => e.Bangtiensi).HasColumnName("bangtiensi");

                entity.Property(e => e.Hedaotao).HasColumnName("hedaotao");

                entity.Property(e => e.Hocham).HasColumnName("hocham");

                entity.Property(e => e.Hocvi).HasColumnName("hocvi");

                entity.Property(e => e.Idnv).HasColumnName("idnv");

                entity.Property(e => e.Namcap).HasColumnName("namcap");

                entity.Property(e => e.Namcapbang).HasColumnName("namcapbang");

                entity.Property(e => e.Namcapbang2).HasColumnName("namcapbang2");

                entity.Property(e => e.Namphong).HasColumnName("namphong");

                entity.Property(e => e.Namtotnghiep).HasColumnName("namtotnghiep");

                entity.Property(e => e.Namtotnghiep2).HasColumnName("namtotnghiep2");

                entity.Property(e => e.Nganhhoc).HasColumnName("nganhhoc");

                entity.Property(e => e.Noidaotao).HasColumnName("noidaotao");

                entity.Property(e => e.Noidaotao2).HasColumnName("noidaotao2");

                entity.Property(e => e.Noidaotaoa2).HasColumnName("noidaotaoa2");

                entity.Property(e => e.Nuocdaotao).HasColumnName("nuocdaotao");

                entity.Property(e => e.Tenchuyende).HasColumnName("tenchuyende");

                entity.HasOne(d => d.IdnvNavigation)
                    .WithMany(p => p.Tbllyliches)
                    .HasForeignKey(d => d.Idnv)
                    .HasConstraintName("FK_tbllylich_tblnhanvien");
            });

            modelBuilder.Entity<Tblnhanvien>(entity =>
            {
                entity.ToTable("tblnhanvien");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bidanh)
                    .HasMaxLength(255)
                    .HasColumnName("bidanh");

                entity.Property(e => e.Cmnd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cmnd");

                entity.Property(e => e.Cngdaotao)
                    .HasMaxLength(255)
                    .HasColumnName("cngdaotao");

                entity.Property(e => e.Dantoc)
                    .HasMaxLength(50)
                    .HasColumnName("dantoc");

                entity.Property(e => e.Datotnghiep)
                    .HasMaxLength(255)
                    .HasColumnName("datotnghiep");

                entity.Property(e => e.Dcttru)
                    .HasMaxLength(255)
                    .HasColumnName("dcttru");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("dienthoai");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Gioitinh).HasColumnName("gioitinh");

                entity.Property(e => e.Hinhanh).HasColumnName("hinhanh");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(255)
                    .HasColumnName("hoten");

                entity.Property(e => e.Htdaotao)
                    .HasMaxLength(255)
                    .HasColumnName("htdaotao");

                entity.Property(e => e.Idchucvu).HasColumnName("idchucvu");

                entity.Property(e => e.Idpban).HasColumnName("idpban");

                entity.Property(e => e.Mantn).HasColumnName("mantn");

                entity.Property(e => e.Ncapcmnd)
                    .HasMaxLength(255)
                    .HasColumnName("ncapcmnd");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("date")
                    .HasColumnName("ngaysinh");

                entity.Property(e => e.Ngdaotao)
                    .HasMaxLength(255)
                    .HasColumnName("ngdaotao");

                entity.Property(e => e.Noidaotao)
                    .HasMaxLength(255)
                    .HasColumnName("noidaotao");

                entity.Property(e => e.Noiohnay)
                    .HasMaxLength(255)
                    .HasColumnName("noiohnay");

                entity.Property(e => e.Noisinh)
                    .HasMaxLength(255)
                    .HasColumnName("noisinh");

                entity.Property(e => e.Quequan)
                    .HasMaxLength(255)
                    .HasColumnName("quequan");

                entity.Property(e => e.Quoctich)
                    .HasMaxLength(50)
                    .HasColumnName("quoctich");

                entity.Property(e => e.Tdcaonhat)
                    .HasMaxLength(255)
                    .HasColumnName("tdcaonhat");

                entity.Property(e => e.Tdhocvan)
                    .HasMaxLength(50)
                    .HasColumnName("tdhocvan");

                entity.Property(e => e.Tinhtrang).HasColumnName("tinhtrang");

                entity.Property(e => e.Tongiao)
                    .HasMaxLength(50)
                    .HasColumnName("tongiao");

                entity.Property(e => e.Tthonnhan).HasColumnName("tthonnhan");

                entity.HasOne(d => d.IdchucvuNavigation)
                    .WithMany(p => p.Tblnhanviens)
                    .HasForeignKey(d => d.Idchucvu)
                    .HasConstraintName("FK_tblnhanvien_tblchucvu");

                entity.HasOne(d => d.IdpbanNavigation)
                    .WithMany(p => p.Tblnhanviens)
                    .HasForeignKey(d => d.Idpban)
                    .HasConstraintName("FK_tblnhanvien_tblphongban");
            });

            modelBuilder.Entity<Tblnhomtg>(entity =>
            {
                entity.ToTable("tblnhomtg");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Chucvu).HasColumnName("chucvu");

                entity.Property(e => e.Iddetai).HasColumnName("iddetai");

                entity.Property(e => e.Idnv).HasColumnName("idnv");

                entity.HasOne(d => d.IddetaiNavigation)
                    .WithMany(p => p.Tblnhomtgs)
                    .HasForeignKey(d => d.Iddetai)
                    .HasConstraintName("FK_tblnhomtg_tbldetai");
            });

            modelBuilder.Entity<Tblphanhoi>(entity =>
            {
                entity.ToTable("tblphanhoi");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(50)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Iddetai).HasColumnName("iddetai");

                entity.Property(e => e.Idnv).HasColumnName("idnv");

                entity.Property(e => e.Ngay)
                    .HasColumnType("date")
                    .HasColumnName("ngay");

                entity.Property(e => e.Noidung).HasColumnName("noidung");
                entity.Property(e => e.Trangthai).HasColumnName("trangthai");
                entity.Property(e => e.Hinhanh).HasColumnName("hinhanh");
                entity.Property(e => e.Token_access).HasColumnName("token_access");
                entity.Property(e => e.Id_social).HasColumnName("id_social");
                entity.Property(e => e.Loai).HasColumnName("loai");
                entity.Property(e => e.Fistname).HasColumnName("fistname");
                entity.Property(e => e.Lastname).HasColumnName("lastname");
                entity.Property(e => e.Number).HasColumnName("number");
                entity.Property(e => e.Ngay_update).HasColumnName("ngay_update");
                entity.Property(e => e.Last_user).HasColumnName("last_user");
                entity.HasOne(d => d.IddetaiNavigation)
                    .WithMany(p => p.Tblphanhois)
                    .HasForeignKey(d => d.Iddetai)
                    .HasConstraintName("FK_tblphanhoi_tbldetai");
            });

            modelBuilder.Entity<Tblphongban>(entity =>
            {
                entity.ToTable("tblphongban");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Iddv).HasColumnName("iddv");

                entity.Property(e => e.Tenphongban).HasColumnName("tenphongban");

                entity.HasOne(d => d.IddvNavigation)
                    .WithMany(p => p.Tblphongbans)
                    .HasForeignKey(d => d.Iddv)
                    .HasConstraintName("FK_tblphongban_tbldonvi");
            });

            modelBuilder.Entity<Tblsohuu>(entity =>
            {
                entity.ToTable("tblsohuu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dm).HasColumnName("dm");

                entity.Property(e => e.Dmhtkinhphi).HasColumnName("dmhtkinhphi");

                entity.Property(e => e.Ghichu).HasColumnName("ghichu");

                entity.Property(e => e.Tensohuu).HasColumnName("tensohuu");
            });

            modelBuilder.Entity<Tblsohuudetai>(entity =>
            {
                entity.ToTable("tblsohuudetai");

                entity.Property(e => e.Id)
                    //.ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ghichu).HasColumnName("ghichu");

                entity.Property(e => e.Iddetai).HasColumnName("iddetai");

                entity.Property(e => e.Idsohuu).HasColumnName("idsohuu");

                entity.HasOne(d => d.IddetaiNavigation)
                    .WithMany(p => p.Tblsohuudetais)
                    .HasForeignKey(d => d.Iddetai)
                    .HasConstraintName("FK_tblsohuudetai_tbldetai");

                entity.HasOne(d => d.IdsohuuNavigation)
                    .WithMany(p => p.Tblsohuudetais)
                    .HasForeignKey(d => d.Idsohuu)
                    .HasConstraintName("FK_tblsohuudetai_tblsohuu");
            });

            modelBuilder.Entity<Tbltintuc>(entity =>
            {
                entity.ToTable("tbltintuc");

                entity.Property(e => e.Id)
                    //.ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Hinhanh)
                    .IsUnicode(false)
                    .HasColumnName("hinhanh");

                entity.Property(e => e.Idloai).HasColumnName("idloai");

                entity.Property(e => e.Luotem).HasColumnName("luotem");

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Tieude).HasColumnName("tieude");

                entity.HasOne(d => d.IdloaiNavigation)
                    .WithMany(p => p.Tbltintucs)
                    .HasForeignKey(d => d.Idloai)
                    .HasConstraintName("FK_tbltintuc_tblloaitt");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Hinhanh)
                    .IsUnicode(false)
                    .HasColumnName("hinhanh");

                entity.Property(e => e.Idnhanvien).HasColumnName("idnhanvien");

                entity.Property(e => e.Idrole).HasColumnName("idrole");

                entity.Property(e => e.Matkhau)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("matkhau");

                entity.Property(e => e.Taikhoan)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taikhoan");

                entity.Property(e => e.Token)
                    .IsUnicode(false)
                    .HasColumnName("token");
                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.IdnhanvienNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Idnhanvien)
                    .HasConstraintName("FK_user_tblnhanvien");

                entity.HasOne(d => d.IdroleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Idrole)
                    .HasConstraintName("FK_user_role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
