﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhongKhamThuCung.Data;

#nullable disable

namespace PhongKhamThuCung.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250320062139_CapNhatLaiBangLan2")]
    partial class CapNhatLaiBangLan2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Active")
                        .HasColumnType("int");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("HoVaTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.BacSi", b =>
                {
                    b.Property<int>("MaBacSi")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaBacSi"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaChuyenKhoa")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenBacSi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaBacSi");

                    b.HasIndex("MaChuyenKhoa");

                    b.ToTable("BacSi");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.ChiTietUuDai", b =>
                {
                    b.Property<int>("MaChiTietUuDai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaChiTietUuDai"));

                    b.Property<int?>("MaHoaDon")
                        .HasColumnType("int");

                    b.Property<int?>("MaUuDai")
                        .HasColumnType("int");

                    b.HasKey("MaChiTietUuDai");

                    b.HasIndex("MaHoaDon");

                    b.HasIndex("MaUuDai");

                    b.ToTable("ChiTietUuDai");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.ChuyenKhoa", b =>
                {
                    b.Property<int>("MaChuyenKhoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaChuyenKhoa"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTaNgan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenChuyenKhoa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaChuyenKhoa");

                    b.ToTable("ChuyenKhoa");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.DanhGia", b =>
                {
                    b.Property<int>("MaDanhGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDanhGia"));

                    b.Property<int?>("Diem")
                        .HasColumnType("int");

                    b.Property<string>("MoTaNgan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MaDanhGia");

                    b.HasIndex("UserId");

                    b.ToTable("DanhGia");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.DichVu", b =>
                {
                    b.Property<int>("MaDichVu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDichVu"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("GiaDichVu")
                        .HasColumnType("float");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenDichVu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaDichVu");

                    b.ToTable("DichVu");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.GioiThieu", b =>
                {
                    b.Property<int>("MaGioiThieu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaGioiThieu"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTaNgan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenGioiThieu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaGioiThieu");

                    b.ToTable("GioiThieu");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.HoaDon", b =>
                {
                    b.Property<int>("MaHoaDon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHoaDon"));

                    b.Property<double?>("DaThanhToan")
                        .HasColumnType("float");

                    b.Property<int?>("MaLH")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayLap")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayThanhToan")
                        .HasColumnType("datetime2");

                    b.Property<double?>("ThanhTien")
                        .HasColumnType("float");

                    b.Property<bool>("TinhTrang")
                        .HasColumnType("bit");

                    b.Property<double?>("TongTien")
                        .HasColumnType("float");

                    b.HasKey("MaHoaDon");

                    b.HasIndex("MaLH")
                        .IsUnique()
                        .HasFilter("[MaLH] IS NOT NULL");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.KhachHang", b =>
                {
                    b.Property<int>("MaKhachHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKhachHang"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaiKhoan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenKhachHang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaKhachHang");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.LichHen", b =>
                {
                    b.Property<int>("MaLichHen")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLichHen"));

                    b.Property<int?>("BacSiMaBacSi")
                        .HasColumnType("int");

                    b.Property<int?>("MaBacSi")
                        .HasColumnType("int");

                    b.Property<int?>("MaDichVu")
                        .HasColumnType("int");

                    b.Property<int?>("MaThuCung")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayHen")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaLichHen");

                    b.HasIndex("BacSiMaBacSi");

                    b.HasIndex("MaDichVu");

                    b.HasIndex("MaThuCung");

                    b.ToTable("LichHen");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.LoaiTinTuc", b =>
                {
                    b.Property<int>("MaLoaiTin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLoaiTin"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenLoai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaLoaiTin");

                    b.ToTable("LoaiTinTuc");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.QuangCao", b =>
                {
                    b.Property<int>("MaQuangCao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaQuangCao"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasMaxLength(49)
                        .HasColumnType("nvarchar(49)");

                    b.Property<string>("NoiDung")
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.HasKey("MaQuangCao");

                    b.ToTable("QuangCao");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.ThuCung", b =>
                {
                    b.Property<int>("MaThuCung")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaThuCung"));

                    b.Property<string>("GiongLoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenThuCung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TinhTrang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Tuoi")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MaThuCung");

                    b.HasIndex("UserId");

                    b.ToTable("ThuCung");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.TinTuc", b =>
                {
                    b.Property<int>("MaTinTuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTinTuc"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaLoaiTin")
                        .HasColumnType("int");

                    b.Property<string>("MoTaNgan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayDang")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TieuDe")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaTinTuc");

                    b.HasIndex("MaLoaiTin");

                    b.ToTable("TinTuc");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.UuDai", b =>
                {
                    b.Property<int>("MaUuDai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaUuDai"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<double?>("GiamGia")
                        .HasColumnType("float");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenUuDai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaUuDai");

                    b.ToTable("UuDai");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PhongKhamThuCung.Models.EF.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PhongKhamThuCung.Models.EF.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhongKhamThuCung.Models.EF.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PhongKhamThuCung.Models.EF.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.BacSi", b =>
                {
                    b.HasOne("PhongKhamThuCung.Models.EF.ChuyenKhoa", "ChuyenKhoa")
                        .WithMany()
                        .HasForeignKey("MaChuyenKhoa");

                    b.Navigation("ChuyenKhoa");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.ChiTietUuDai", b =>
                {
                    b.HasOne("PhongKhamThuCung.Models.EF.HoaDon", "HoaDon")
                        .WithMany()
                        .HasForeignKey("MaHoaDon");

                    b.HasOne("PhongKhamThuCung.Models.EF.UuDai", "UuDai")
                        .WithMany()
                        .HasForeignKey("MaUuDai");

                    b.Navigation("HoaDon");

                    b.Navigation("UuDai");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.DanhGia", b =>
                {
                    b.HasOne("PhongKhamThuCung.Models.EF.ApplicationUser", "ApplicationUser")
                        .WithMany("DanhGias")
                        .HasForeignKey("UserId");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.HoaDon", b =>
                {
                    b.HasOne("PhongKhamThuCung.Models.EF.LichHen", "LichHen")
                        .WithOne("HoaDon")
                        .HasForeignKey("PhongKhamThuCung.Models.EF.HoaDon", "MaLH");

                    b.Navigation("LichHen");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.LichHen", b =>
                {
                    b.HasOne("PhongKhamThuCung.Models.EF.BacSi", "BacSi")
                        .WithMany("LichHen")
                        .HasForeignKey("BacSiMaBacSi");

                    b.HasOne("PhongKhamThuCung.Models.EF.DichVu", "DichVu")
                        .WithMany("LichHens")
                        .HasForeignKey("MaDichVu");

                    b.HasOne("PhongKhamThuCung.Models.EF.ThuCung", "ThuCung")
                        .WithMany()
                        .HasForeignKey("MaThuCung");

                    b.Navigation("BacSi");

                    b.Navigation("DichVu");

                    b.Navigation("ThuCung");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.ThuCung", b =>
                {
                    b.HasOne("PhongKhamThuCung.Models.EF.ApplicationUser", "ApplicationUser")
                        .WithMany("ThuCungs")
                        .HasForeignKey("UserId");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.TinTuc", b =>
                {
                    b.HasOne("PhongKhamThuCung.Models.EF.LoaiTinTuc", "LoaiTinTuc")
                        .WithMany("TinTucs")
                        .HasForeignKey("MaLoaiTin");

                    b.Navigation("LoaiTinTuc");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.ApplicationUser", b =>
                {
                    b.Navigation("DanhGias");

                    b.Navigation("ThuCungs");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.BacSi", b =>
                {
                    b.Navigation("LichHen");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.DichVu", b =>
                {
                    b.Navigation("LichHens");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.LichHen", b =>
                {
                    b.Navigation("HoaDon");
                });

            modelBuilder.Entity("PhongKhamThuCung.Models.EF.LoaiTinTuc", b =>
                {
                    b.Navigation("TinTucs");
                });
#pragma warning restore 612, 618
        }
    }
}
