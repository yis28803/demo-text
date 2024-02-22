using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duanmaulan4.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDemvaTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HanhKiem",
                columns: table => new
                {
                    MaHanhKiem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHanhKiem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HanhKiem", x => x.MaHanhKiem);
                });

            migrationBuilder.CreateTable(
                name: "HocKy",
                columns: table => new
                {
                    MaHocKy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHocKy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeSo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocKy", x => x.MaHocKy);
                });

            migrationBuilder.CreateTable(
                name: "HocLuc",
                columns: table => new
                {
                    MaHocLuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHocLuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiemCanDuoi = table.Column<int>(type: "int", nullable: false),
                    DiemCanTren = table.Column<int>(type: "int", nullable: false),
                    DiemKhongChe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocLuc", x => x.MaHocLuc);
                });

            migrationBuilder.CreateTable(
                name: "KetQua",
                columns: table => new
                {
                    MaKetQua = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKetQua = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KetQua", x => x.MaKetQua);
                });

            migrationBuilder.CreateTable(
                name: "KhoaKhoi",
                columns: table => new
                {
                    MaKhoaKhoi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhoaKhoi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoaKhoi", x => x.MaKhoaKhoi);
                });

            migrationBuilder.CreateTable(
                name: "LoaiDiem",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeSo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiDiem", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "LoaiHocPhi",
                columns: table => new
                {
                    MaLoaiHocPhi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiHocPhi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHocPhi", x => x.MaLoaiHocPhi);
                });

            migrationBuilder.CreateTable(
                name: "NienKhoa",
                columns: table => new
                {
                    MaNienKhoa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNienKhoa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NienKhoa", x => x.MaNienKhoa);
                });

            migrationBuilder.CreateTable(
                name: "ToBoMon",
                columns: table => new
                {
                    MaToBoMon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenToBoMon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToBoMon", x => x.MaToBoMon);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lop",
                columns: table => new
                {
                    MaLop = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaKhoaKhoi = table.Column<int>(type: "int", nullable: false),
                    MaNienKhoa = table.Column<int>(type: "int", nullable: false),
                    SoLuongHocSinh = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    HocPhi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lop", x => x.MaLop);
                    table.ForeignKey(
                        name: "FK_Lop_KhoaKhoi_MaKhoaKhoi",
                        column: x => x.MaKhoaKhoi,
                        principalTable: "KhoaKhoi",
                        principalColumn: "MaKhoaKhoi",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lop_NienKhoa_MaNienKhoa",
                        column: x => x.MaNienKhoa,
                        principalTable: "NienKhoa",
                        principalColumn: "MaNienKhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MonHoc",
                columns: table => new
                {
                    MaMonHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMonHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaKhoaKhoi = table.Column<int>(type: "int", nullable: false),
                    MaToBoMon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHoc", x => x.MaMonHoc);
                    table.ForeignKey(
                        name: "FK_MonHoc_KhoaKhoi_MaKhoaKhoi",
                        column: x => x.MaKhoaKhoi,
                        principalTable: "KhoaKhoi",
                        principalColumn: "MaKhoaKhoi",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonHoc_ToBoMon_MaToBoMon",
                        column: x => x.MaToBoMon,
                        principalTable: "ToBoMon",
                        principalColumn: "MaToBoMon",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KqLopHocHocky",
                columns: table => new
                {
                    MaLop = table.Column<int>(type: "int", nullable: false),
                    MaNienKhoa = table.Column<int>(type: "int", nullable: false),
                    MaHocKy = table.Column<int>(type: "int", nullable: false),
                    SoLuongDat = table.Column<int>(type: "int", nullable: false),
                    TiLe = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KqLopHocHocky", x => new { x.MaLop, x.MaNienKhoa, x.MaHocKy });
                    table.ForeignKey(
                        name: "FK_KqLopHocHocky_HocKy_MaHocKy",
                        column: x => x.MaHocKy,
                        principalTable: "HocKy",
                        principalColumn: "MaHocKy",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqLopHocHocky_Lop_MaLop",
                        column: x => x.MaLop,
                        principalTable: "Lop",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqLopHocHocky_NienKhoa_MaNienKhoa",
                        column: x => x.MaNienKhoa,
                        principalTable: "NienKhoa",
                        principalColumn: "MaNienKhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GiaoVien",
                columns: table => new
                {
                    MaGiaoVien = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaSoThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDemvaTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaMonHoc = table.Column<int>(type: "int", nullable: false),
                    MonKiemNhiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoVien", x => x.MaGiaoVien);
                    table.ForeignKey(
                        name: "FK_GiaoVien_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiaoVien_MonHoc_MaMonHoc",
                        column: x => x.MaMonHoc,
                        principalTable: "MonHoc",
                        principalColumn: "MaMonHoc",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KqLopHocMonHoc",
                columns: table => new
                {
                    MaLop = table.Column<int>(type: "int", nullable: false),
                    MaNienKhoa = table.Column<int>(type: "int", nullable: false),
                    MaMonHoc = table.Column<int>(type: "int", nullable: false),
                    MaHocKy = table.Column<int>(type: "int", nullable: false),
                    SoLuongDat = table.Column<int>(type: "int", nullable: false),
                    TiLe = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KqLopHocMonHoc", x => new { x.MaMonHoc, x.MaLop, x.MaNienKhoa, x.MaHocKy });
                    table.ForeignKey(
                        name: "FK_KqLopHocMonHoc_HocKy_MaHocKy",
                        column: x => x.MaHocKy,
                        principalTable: "HocKy",
                        principalColumn: "MaHocKy",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqLopHocMonHoc_Lop_MaLop",
                        column: x => x.MaLop,
                        principalTable: "Lop",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqLopHocMonHoc_MonHoc_MaMonHoc",
                        column: x => x.MaMonHoc,
                        principalTable: "MonHoc",
                        principalColumn: "MaMonHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqLopHocMonHoc_NienKhoa_MaNienKhoa",
                        column: x => x.MaNienKhoa,
                        principalTable: "NienKhoa",
                        principalColumn: "MaNienKhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhanCong",
                columns: table => new
                {
                    MaPhanCong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLop = table.Column<int>(type: "int", nullable: false),
                    MaMonHoc = table.Column<int>(type: "int", nullable: false),
                    MaGiaoVien = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhongHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianBatDau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianKetThuc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCong", x => x.MaPhanCong);
                    table.ForeignKey(
                        name: "FK_PhanCong_GiaoVien_MaGiaoVien",
                        column: x => x.MaGiaoVien,
                        principalTable: "GiaoVien",
                        principalColumn: "MaGiaoVien",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhanCong_Lop_MaLop",
                        column: x => x.MaLop,
                        principalTable: "Lop",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhanCong_MonHoc_MaMonHoc",
                        column: x => x.MaMonHoc,
                        principalTable: "MonHoc",
                        principalColumn: "MaMonHoc",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HocSinh",
                columns: table => new
                {
                    MaHocSinh = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Ho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDemvaTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoTenPhuHuynh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaPhanCong = table.Column<int>(type: "int", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocSinh", x => x.MaHocSinh);
                    table.ForeignKey(
                        name: "FK_HocSinh_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HocSinh_PhanCong_MaPhanCong",
                        column: x => x.MaPhanCong,
                        principalTable: "PhanCong",
                        principalColumn: "MaPhanCong",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Diem",
                columns: table => new
                {
                    STT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocSinh = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaMonHoc = table.Column<int>(type: "int", nullable: false),
                    MaHocKy = table.Column<int>(type: "int", nullable: false),
                    MaNienKhoa = table.Column<int>(type: "int", nullable: false),
                    MaLop = table.Column<int>(type: "int", nullable: false),
                    MaLoai = table.Column<int>(type: "int", nullable: false),
                    Diem = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diem", x => x.STT);
                    table.ForeignKey(
                        name: "FK_Diem_HocKy_MaHocKy",
                        column: x => x.MaHocKy,
                        principalTable: "HocKy",
                        principalColumn: "MaHocKy",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Diem_HocSinh_MaHocSinh",
                        column: x => x.MaHocSinh,
                        principalTable: "HocSinh",
                        principalColumn: "MaHocSinh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Diem_LoaiDiem_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "LoaiDiem",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Diem_Lop_MaLop",
                        column: x => x.MaLop,
                        principalTable: "Lop",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Diem_MonHoc_MaMonHoc",
                        column: x => x.MaMonHoc,
                        principalTable: "MonHoc",
                        principalColumn: "MaMonHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Diem_NienKhoa_MaNienKhoa",
                        column: x => x.MaNienKhoa,
                        principalTable: "NienKhoa",
                        principalColumn: "MaNienKhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KqHocSinhCaNam",
                columns: table => new
                {
                    MaHocSinh = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaLop = table.Column<int>(type: "int", nullable: false),
                    MaNienKhoa = table.Column<int>(type: "int", nullable: false),
                    MaHocLuc = table.Column<int>(type: "int", nullable: false),
                    MaHanhKiem = table.Column<int>(type: "int", nullable: false),
                    MaKetQua = table.Column<int>(type: "int", nullable: false),
                    DiemTBHK1 = table.Column<float>(type: "real", nullable: false),
                    DiemTBHK2 = table.Column<float>(type: "real", nullable: false),
                    DiemTBCN = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KqHocSinhCaNam", x => new { x.MaHocSinh, x.MaLop, x.MaNienKhoa });
                    table.ForeignKey(
                        name: "FK_KqHocSinhCaNam_HanhKiem_MaHanhKiem",
                        column: x => x.MaHanhKiem,
                        principalTable: "HanhKiem",
                        principalColumn: "MaHanhKiem",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqHocSinhCaNam_HocLuc_MaHocLuc",
                        column: x => x.MaHocLuc,
                        principalTable: "HocLuc",
                        principalColumn: "MaHocLuc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqHocSinhCaNam_HocSinh_MaHocSinh",
                        column: x => x.MaHocSinh,
                        principalTable: "HocSinh",
                        principalColumn: "MaHocSinh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqHocSinhCaNam_KetQua_MaKetQua",
                        column: x => x.MaKetQua,
                        principalTable: "KetQua",
                        principalColumn: "MaKetQua",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqHocSinhCaNam_Lop_MaLop",
                        column: x => x.MaLop,
                        principalTable: "Lop",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqHocSinhCaNam_NienKhoa_MaNienKhoa",
                        column: x => x.MaNienKhoa,
                        principalTable: "NienKhoa",
                        principalColumn: "MaNienKhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KqHocSinhMonHoc",
                columns: table => new
                {
                    MaHocSinh = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaLop = table.Column<int>(type: "int", nullable: false),
                    MaNienKhoa = table.Column<int>(type: "int", nullable: false),
                    MaMonHoc = table.Column<int>(type: "int", nullable: false),
                    MaHocKy = table.Column<int>(type: "int", nullable: false),
                    DiemMiengTB = table.Column<float>(type: "real", nullable: false),
                    Diem15PhutTB = table.Column<float>(type: "real", nullable: false),
                    Diem45PhutTB = table.Column<float>(type: "real", nullable: false),
                    DiemThi = table.Column<float>(type: "real", nullable: false),
                    DiemTBHK = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KqHocSinhMonHoc", x => new { x.MaHocSinh, x.MaLop, x.MaNienKhoa, x.MaMonHoc, x.MaHocKy });
                    table.ForeignKey(
                        name: "FK_KqHocSinhMonHoc_HocKy_MaHocKy",
                        column: x => x.MaHocKy,
                        principalTable: "HocKy",
                        principalColumn: "MaHocKy",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqHocSinhMonHoc_HocSinh_MaHocSinh",
                        column: x => x.MaHocSinh,
                        principalTable: "HocSinh",
                        principalColumn: "MaHocSinh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqHocSinhMonHoc_Lop_MaLop",
                        column: x => x.MaLop,
                        principalTable: "Lop",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqHocSinhMonHoc_MonHoc_MaMonHoc",
                        column: x => x.MaMonHoc,
                        principalTable: "MonHoc",
                        principalColumn: "MaMonHoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KqHocSinhMonHoc_NienKhoa_MaNienKhoa",
                        column: x => x.MaNienKhoa,
                        principalTable: "NienKhoa",
                        principalColumn: "MaNienKhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhanLop",
                columns: table => new
                {
                    MaPhanCong = table.Column<int>(type: "int", nullable: false),
                    MaHocSinh = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhanLopId = table.Column<int>(type: "int", nullable: false),
                    TinhTrangHocPhi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanLop", x => new { x.MaHocSinh, x.MaPhanCong });
                    table.ForeignKey(
                        name: "FK_PhanLop_HocSinh_MaHocSinh",
                        column: x => x.MaHocSinh,
                        principalTable: "HocSinh",
                        principalColumn: "MaHocSinh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhanLop_PhanCong_MaPhanCong",
                        column: x => x.MaPhanCong,
                        principalTable: "PhanCong",
                        principalColumn: "MaPhanCong",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThuHocPhi",
                columns: table => new
                {
                    MaHocPhi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocSinh = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaLop = table.Column<int>(type: "int", nullable: false),
                    MaLoaiHocPhi = table.Column<int>(type: "int", nullable: false),
                    MucThuPhi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThuPhi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuHocPhi", x => x.MaHocPhi);
                    table.ForeignKey(
                        name: "FK_ThuHocPhi_HocSinh_MaHocSinh",
                        column: x => x.MaHocSinh,
                        principalTable: "HocSinh",
                        principalColumn: "MaHocSinh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThuHocPhi_LoaiHocPhi_MaLoaiHocPhi",
                        column: x => x.MaLoaiHocPhi,
                        principalTable: "LoaiHocPhi",
                        principalColumn: "MaLoaiHocPhi",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThuHocPhi_Lop_MaLop",
                        column: x => x.MaLop,
                        principalTable: "Lop",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Diem_MaHocKy",
                table: "Diem",
                column: "MaHocKy");

            migrationBuilder.CreateIndex(
                name: "IX_Diem_MaHocSinh",
                table: "Diem",
                column: "MaHocSinh");

            migrationBuilder.CreateIndex(
                name: "IX_Diem_MaLoai",
                table: "Diem",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_Diem_MaLop",
                table: "Diem",
                column: "MaLop");

            migrationBuilder.CreateIndex(
                name: "IX_Diem_MaMonHoc",
                table: "Diem",
                column: "MaMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_Diem_MaNienKhoa",
                table: "Diem",
                column: "MaNienKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoVien_MaMonHoc",
                table: "GiaoVien",
                column: "MaMonHoc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiaoVien_UserId",
                table: "GiaoVien",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HocSinh_MaPhanCong",
                table: "HocSinh",
                column: "MaPhanCong");

            migrationBuilder.CreateIndex(
                name: "IX_HocSinh_UserId",
                table: "HocSinh",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KqHocSinhCaNam_MaHanhKiem",
                table: "KqHocSinhCaNam",
                column: "MaHanhKiem");

            migrationBuilder.CreateIndex(
                name: "IX_KqHocSinhCaNam_MaHocLuc",
                table: "KqHocSinhCaNam",
                column: "MaHocLuc");

            migrationBuilder.CreateIndex(
                name: "IX_KqHocSinhCaNam_MaKetQua",
                table: "KqHocSinhCaNam",
                column: "MaKetQua");

            migrationBuilder.CreateIndex(
                name: "IX_KqHocSinhCaNam_MaLop",
                table: "KqHocSinhCaNam",
                column: "MaLop");

            migrationBuilder.CreateIndex(
                name: "IX_KqHocSinhCaNam_MaNienKhoa",
                table: "KqHocSinhCaNam",
                column: "MaNienKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_KqHocSinhMonHoc_MaHocKy",
                table: "KqHocSinhMonHoc",
                column: "MaHocKy");

            migrationBuilder.CreateIndex(
                name: "IX_KqHocSinhMonHoc_MaLop",
                table: "KqHocSinhMonHoc",
                column: "MaLop");

            migrationBuilder.CreateIndex(
                name: "IX_KqHocSinhMonHoc_MaMonHoc",
                table: "KqHocSinhMonHoc",
                column: "MaMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_KqHocSinhMonHoc_MaNienKhoa",
                table: "KqHocSinhMonHoc",
                column: "MaNienKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_KqLopHocHocky_MaHocKy",
                table: "KqLopHocHocky",
                column: "MaHocKy");

            migrationBuilder.CreateIndex(
                name: "IX_KqLopHocHocky_MaNienKhoa",
                table: "KqLopHocHocky",
                column: "MaNienKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_KqLopHocMonHoc_MaHocKy",
                table: "KqLopHocMonHoc",
                column: "MaHocKy");

            migrationBuilder.CreateIndex(
                name: "IX_KqLopHocMonHoc_MaLop",
                table: "KqLopHocMonHoc",
                column: "MaLop");

            migrationBuilder.CreateIndex(
                name: "IX_KqLopHocMonHoc_MaNienKhoa",
                table: "KqLopHocMonHoc",
                column: "MaNienKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_Lop_MaKhoaKhoi",
                table: "Lop",
                column: "MaKhoaKhoi");

            migrationBuilder.CreateIndex(
                name: "IX_Lop_MaNienKhoa",
                table: "Lop",
                column: "MaNienKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_MonHoc_MaKhoaKhoi",
                table: "MonHoc",
                column: "MaKhoaKhoi");

            migrationBuilder.CreateIndex(
                name: "IX_MonHoc_MaToBoMon",
                table: "MonHoc",
                column: "MaToBoMon");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_MaGiaoVien",
                table: "PhanCong",
                column: "MaGiaoVien");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_MaLop",
                table: "PhanCong",
                column: "MaLop");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_MaMonHoc",
                table: "PhanCong",
                column: "MaMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_PhanLop_MaPhanCong",
                table: "PhanLop",
                column: "MaPhanCong");

            migrationBuilder.CreateIndex(
                name: "IX_ThuHocPhi_MaHocSinh",
                table: "ThuHocPhi",
                column: "MaHocSinh");

            migrationBuilder.CreateIndex(
                name: "IX_ThuHocPhi_MaLoaiHocPhi",
                table: "ThuHocPhi",
                column: "MaLoaiHocPhi");

            migrationBuilder.CreateIndex(
                name: "IX_ThuHocPhi_MaLop",
                table: "ThuHocPhi",
                column: "MaLop");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Diem");

            migrationBuilder.DropTable(
                name: "KqHocSinhCaNam");

            migrationBuilder.DropTable(
                name: "KqHocSinhMonHoc");

            migrationBuilder.DropTable(
                name: "KqLopHocHocky");

            migrationBuilder.DropTable(
                name: "KqLopHocMonHoc");

            migrationBuilder.DropTable(
                name: "PhanLop");

            migrationBuilder.DropTable(
                name: "ThuHocPhi");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "LoaiDiem");

            migrationBuilder.DropTable(
                name: "HanhKiem");

            migrationBuilder.DropTable(
                name: "HocLuc");

            migrationBuilder.DropTable(
                name: "KetQua");

            migrationBuilder.DropTable(
                name: "HocKy");

            migrationBuilder.DropTable(
                name: "HocSinh");

            migrationBuilder.DropTable(
                name: "LoaiHocPhi");

            migrationBuilder.DropTable(
                name: "PhanCong");

            migrationBuilder.DropTable(
                name: "GiaoVien");

            migrationBuilder.DropTable(
                name: "Lop");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MonHoc");

            migrationBuilder.DropTable(
                name: "NienKhoa");

            migrationBuilder.DropTable(
                name: "KhoaKhoi");

            migrationBuilder.DropTable(
                name: "ToBoMon");
        }
    }
}
