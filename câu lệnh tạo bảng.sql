CREATE   database Quan_Li_Nhan_Vien
go
use Quan_Li_Nhan_Vien
go
create table NhanSu
(
	NS_Ma char(10) primary key,
	NS_HoVaTen nvarchar(50) NOT NULL,
	NS_GioiTinh BIT,
	NS_NgaySinh DATE,
	NS_SoDienThoai char(10),
	NS_Email varchar(100) NOT NULL,
	NS_DiaChi NTEXT NOT NULL,
	NS_SoCCCD char(12) NOT NULL,
	NS_SoTaiKhoanNganHang INT,
	NS_TenChuTaiKhoan VARCHAR(50),
	NS_HocVan NVARCHAR(20),
	NS_NgayVao DATE NOT NULL
);
create table PhongBan
(
	PB_Ma char(10) primary key ,
	PB_Ten nvarchar(50) NOT NULL,
	PB_VaiTro NVARCHAR(50),
	NS_Ma CHAR(10)
);
create table BoPhan(
	BP_Ma char(10) primary key ,
	BP_Ten nvarchar(50) NOT NULL,
	BP_ChuyenMon NVARCHAR(50),
	PB_Ma char(10) NOT NULL,
	NS_Ma CHAR(10)
);
create table BaoHiem(
	BH_Ma char(10) primary key,
	BH_SoBaoHiem CHAR(10) NOT NULL,
	BH_NgayCap DATE NOT NULL,
	BH_NgayHetHan DATE NOT NULL,
	BH_NoiCap NVARCHAR(50) NOT NULL,
	BH_NoiKhamBenh NVARCHAR(50),
	NS_Ma char(10) NOT NULL,
	CHECK(BH_NgayCap < BH_NgayHetHan)
);
create table TaiKhoan
(
	TK_TenDangNhap varchar(30) primary KEY,
	TK_MatKhau text not null,
	TK_QuyenAdmin BIT DEFAULT 0,
	TK_QuyenQuanLy BIT DEFAULT 0,
	TK_BiKhoa BIT DEFAULT 0,
	TK_ThoiGianMoKhoa DATETIME,
	TK_MaXacThuc char(6),
	TK_ThoiGianTaoMa datetime,
	TK_AnhDaiDien TEXT,
	NS_Ma char(10) NOT NULL unique
);
create table KhenThuongKyluat(
	KTKL_Ma CHAR(10) PRIMARY KEY,
	KTKL_MoTa ntext,
	KTKL_ThoiGian datetime,
	KTKL_HinhThuc NVARCHAR(20),
	KTKL_SoTien FLOAT NOT NULL,
	NS_Ma char(10)
);
create table LichSuHanhDong(
	LSHD_Ma char(10) primary key,
	LSHD_TieuDe nvarchar(30),
	LSHD_MoTa ntext,
	LSHD_ThoiGian DATETIME,
	NS_Ma char(10)
);
create table ChamCong
(
	CC_Ma CHAR(10) PRIMARY KEY,
	CC_ThoiGianDen datetime not null,
	CC_ThoiGianVe DATETIME,
	CC_Ngay DATE NOT NULL,
	NS_Ma char(10) NOT NULL,
	CHECK(CC_ThoiGianDen < CC_ThoiGianVe)
);
CREATE TABLE CaLam(
	CL_Ma CHAR(10) PRIMARY KEY,
	CL_TenCa NVARCHAR(30),
	CL_GioBatDau SMALLINT CHECK(CL_GioBatDau >= 0 AND CL_GioBatDau <= 23),
	CL_PhutBatDau SMALLINT CHECK(CL_PhutBatDau >= 1 AND CL_PhutBatDau <= 60),
	CL_GioKetThuc SMALLINT CHECK(CL_GioKetThuc >= 0 AND CL_GioKetThuc <= 23),
	CL_PhutKetThuc SMALLINT CHECK(CL_PhutKetThuc >= 1 AND CL_PhutKetThuc <= 60),
	CHECK(CL_GioBatDau < CL_GioKetThuc)
);
CREATE TABLE DangKyCaLam(
	DKCL_Ma CHAR(10) PRIMARY KEY,
	DKCL_Ngay DATE NOT NULL,
	DKCL_ThoiGianDangKy DATETIME,
	DKCL_DaDuocDuyet BIT DEFAULT 0,
	NS_Ma CHAR(10) NOT NULL,
	CL_Ma CHAR(10),
	CHECK(DKCL_ThoiGianDangKy < DKCL_Ngay)
);
CREATE TABLE DangKyNghiLam (
	DKNL_Ma CHAR(10) PRIMARY KEY,
	DKNL_Ngay DATE NOT NULL,
	DKNL_ThoiGianDangKy DATE,
	DKNL_NghiCoPhep BIT DEFAULT 0,
	DKNL_LyDoNghi NTEXT,
	DKNL_DaDuocDuyet BIT DEFAULT 0,
	NS_Ma CHAR(10) NOT NULL,
	DDK_Ma CHAR(10),
	CHECK(DKNL_ThoiGianDangKy < DKNL_Ngay)
);
CREATE TABLE DuyetDangKy(
	DDK_Ma CHAR(10) PRIMARY KEY,
	DDK_ThoiGian DATETIME,
	DKCL_Ma CHAR(10),
	DKNL_Ma CHAR(10),
	NS_Ma CHAR(10)
);
 CREATE TABLE HopDong(
	 HD_Ma CHAR(10) PRIMARY KEY,
	 HD_NgayBatDau DATE NOT NULL,
	 HD_NgayKetThuc DATE,
	 HD_HinhThucLamViec NVARCHAR(30),
	 HD_Luong FLOAT NOT NULL,
	 HD_DonViTinhuong NVARCHAR(20) NOT NULL,
	 HD_CongViec NVARCHAR(30),
	 NS_Ma CHAR(10) NOT NULL, 
	 CHECK(HD_NgayBatDau < HD_NgayKetThuc)
 );
 CREATE TABLE LichSuLamViec (
	 LSLV_Ma CHAR(10) PRIMARY KEY,
	 LSLV_NgayBatDau DATE,
	 LSLV_NgayKetThuc DATE,
	 LSLV_ChucVu NVARCHAR(50),
	 NS_Ma CHAR(10) NOT NULL,
	 BP_Ma CHAR(10),
	 CHECK(LSLV_NgayBatDau < LSLV_NgayKetThuc)
 );
 CREATE TABLE NgayNghi 
 (
	NN_Ngay DATE PRIMARY KEY,
	NN_GhiChu NVARCHAR(50)
 )

ALTER TABLE BaoHiem 
ADD CONSTRAINT BH_FK1 FOREIGN KEY 
(NS_Ma) REFERENCES NhanSu(NS_Ma) 

ALTER TABLE TaiKhoan 
ADD CONSTRAINT TK_FK1 FOREIGN KEY 
(NS_Ma) REFERENCES NhanSu(NS_Ma) 

ALTER TABLE ChamCong 
ADD CONSTRAINT CC_FK1 FOREIGN KEY 
(NS_Ma) REFERENCES NhanSu(NS_Ma) 

ALTER TABLE LichSuHanhDong 
ADD CONSTRAINT LSHD_FK1 FOREIGN KEY 
(NS_Ma) REFERENCES NhanSu(NS_Ma) 

ALTER TABLE dbo.KhenThuongKyluat
ADD CONSTRAINT KTKL_FK1 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)

ALTER TABLE dbo.DangKyCaLam ADD
CONSTRAINT DKCL_FK1 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma),
CONSTRAINT DKCL_KF2 FOREIGN KEY(CL_Ma)
REFERENCES dbo.CaLam(CL_Ma) 

ALTER TABLE dbo.DuyetDangKy ADD
CONSTRAINT DDK_FK1 FOREIGN KEY(DKCL_Ma)
REFERENCES dbo.DangKyCaLam(DKCL_Ma),
CONSTRAINT DDK_FK2 FOREIGN KEY(DKNL_Ma)
REFERENCES dbo.DangKyNghiLam(DKNL_Ma),
CONSTRAINT DDK_FK3 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)

ALTER TABLE dbo.DangKyNghiLam ADD 
CONSTRAINT DKNL_FK1 FOREIGN KEY(DDK_Ma)
REFERENCES dbo.DuyetDangKy(DDK_Ma),
CONSTRAINT DKNL_FK2 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)

ALTER TABLE dbo.HopDong ADD
CONSTRAINT HD_FK11 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)

ALTER TABLE dbo.LichSuLamViec ADD
CONSTRAINT LSLV_FK1 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma),
CONSTRAINT LSLV_FK2 FOREIGN KEY(BP_Ma)
REFERENCES dbo.BoPhan(BP_Ma)

ALTER TABLE dbo.BoPhan ADD
CONSTRAINT BP_FK1 FOREIGN KEY(PB_Ma)
REFERENCES dbo.PhongBan(PB_Ma),
CONSTRAINT BP_FK2 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)

ALTER TABLE dbo.PhongBan ADD
CONSTRAINT PB_FK11 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)