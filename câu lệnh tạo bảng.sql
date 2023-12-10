﻿CREATE   database Quan_Ly_Nhan_Su
go
use Quan_Ly_Nhan_Su
go
create table NhanSu
(
	NS_Ma char(10) PRIMARY KEY,
	NS_HoVaTen nvarchar(50) NOT NULL,
	NS_GioiTinh BIT NOT NULL,
	NS_NgaySinh DATE NOT NULL,
	NS_SoDienThoai char(10) NOT NULL,
	NS_Email varchar(100) NOT NULL,
	NS_DiaChi NTEXT NOT NULL,
	NS_SoCCCD char(12) NOT NULL,
	NS_SoTaiKhoanNganHang CHAR(10),
	NS_TenChuTaiKhoan VARCHAR(50),
	NS_HocVan NVARCHAR(20),
	NS_NgayVao DATE NOT NULL
);
create table PhongBan
(
	PB_Ma char(10) PRIMARY KEY,
	PB_Ten nvarchar(50) NOT NULL,
	PB_VaiTro NVARCHAR(50),
	NS_Ma CHAR(10)
);
create table BoPhan(
	BP_Ma char(10) PRIMARY KEY,
	BP_Ten nvarchar(50) NOT NULL,
	BP_ChuyenMon NVARCHAR(50),
	PB_Ma char(10) NOT NULL,
	NS_Ma CHAR(10)
);
create table BaoHiem
(
	BH_Ma BIGINT IDENTITY PRIMARY KEY,
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
	TK_TenDangNhap varchar(30) PRIMARY KEY,
	TK_MatKhau text not null,
	TK_QuyenAdmin BIT NOT NULL DEFAULT 0,
	TK_QuyenQuanLy BIT NOT NULL DEFAULT 0,
	TK_BiKhoa BIT NOT NULL DEFAULT 0,
	TK_ThoiGianMoKhoa DATETIME,
	TK_MaXacThuc char(6),
	TK_ThoiGianTaoMa datetime,
	TK_AnhDaiDien TEXT,
	NS_Ma char(10) NOT NULL unique
);
create table KhenThuongKyLuat
(
	KTKL_Ma BIGINT IDENTITY PRIMARY KEY,
	KTKL_MoTa ntext,
	KTKL_ThoiGian datetime,
	KTKL_HinhThuc NVARCHAR(20) NOT NULL,
	KTKL_SoTien FLOAT NOT NULL,
	NS_Ma char(10) NOT NULL
);
create table ChamCong
(
	CC_Ma BIGINT IDENTITY PRIMARY KEY,
	CC_ThoiGianDen datetime not null,
	CC_ThoiGianVe DATETIME,
	CC_Ngay DATE NOT NULL,
	NS_Ma char(10) NOT NULL,
	CHECK(CC_ThoiGianDen < CC_ThoiGianVe)
);
CREATE TABLE CaLam
(
	CL_Ma CHAR(10) PRIMARY KEY,
	CL_TenCa NVARCHAR(30) NOT NULL,
	CL_GioBatDau SMALLINT NOT NULL CHECK(CL_GioBatDau >= 0 AND CL_GioBatDau <= 23),
	CL_PhutBatDau SMALLINT NOT NULL CHECK(CL_PhutBatDau >= 0 AND CL_PhutBatDau <= 60),
	CL_GioKetThuc SMALLINT NOT NULL CHECK(CL_GioKetThuc >= 0 AND CL_GioKetThuc <= 23),
	CL_PhutKetThuc SMALLINT NOT NULL CHECK(CL_PhutKetThuc >= 0 AND CL_PhutKetThuc <= 60),
	CHECK(CL_GioBatDau < CL_GioKetThuc)
);
CREATE TABLE DangKyCaLam
(
	DKCL_Ma BIGINT IDENTITY PRIMARY KEY,
	DKCL_Ngay DATE NOT NULL,
	DKCL_ThoiGianDangKy DATETIME,
	DKCL_DaDuocDuyet BIT NOT NULL DEFAULT 0,
	NS_Ma CHAR(10) NOT NULL,
	CL_Ma CHAR(10) NOT NULL,
	DDK_Ma BIGINT,
	CHECK(DKCL_ThoiGianDangKy < DKCL_Ngay)
);
CREATE TABLE DangKyNghiLam 
(
	DKNL_Ma BIGINT IDENTITY PRIMARY KEY,
	DKNL_Ngay DATE NOT NULL,
	DKNL_ThoiGianDangKy DATE,
	DKNL_NghiCoPhep BIT NOT NULL DEFAULT 0,
	DKNL_LyDoNghi NTEXT,
	DKNL_DaDuocDuyet BIT NOT NULL DEFAULT 0,
	NS_Ma CHAR(10) NOT NULL,
	DDK_Ma BIGINT,
	CHECK(DKNL_ThoiGianDangKy < DKNL_Ngay)
);
CREATE TABLE DuyetDangKy
(
	DDK_Ma BIGINT IDENTITY PRIMARY KEY,
	DDK_ThoiGian DATETIME,
	NS_Ma CHAR(10)
);
 CREATE TABLE HopDong
(
	 HD_Ma BIGINT IDENTITY PRIMARY KEY,
	 HD_NgayBatDau DATE NOT NULL,
	 HD_NgayKetThuc DATE,
	 HD_HinhThucLamViec NVARCHAR(30) NOT NULL,
	 HD_Luong FLOAT NOT NULL,
	 HD_DonViTinhuong NVARCHAR(20) NOT NULL,
	 HD_CongViec NVARCHAR(30) NOT NULL,
	 NS_Ma CHAR(10) NOT NULL, 
	 CHECK(HD_NgayBatDau < HD_NgayKetThuc)
 );
 CREATE TABLE LichSuLamViec 
(
	 LSLV_Ma BIGINT IDENTITY PRIMARY KEY,
	 LSLV_NgayBatDau DATE NOT NULL,
	 LSLV_NgayKetThuc DATE,
	 LSLV_ChucVu NVARCHAR(50) NOT NULL,
	 NS_Ma CHAR(10) NOT NULL,
	 BP_Ma CHAR(10),
	 CHECK(LSLV_NgayBatDau < LSLV_NgayKetThuc)
 );
 CREATE TABLE NgayNghi 
 (
	NN_Ma BIGINT IDENTITY PRIMARY KEY,
	NN_Ngay DATE NOT NULL UNIQUE,
	NN_GhiChu NVARCHAR(50)
 )

ALTER TABLE BaoHiem 
ADD CONSTRAINT BH_FK1 FOREIGN KEY 
(NS_Ma) REFERENCES NhanSu(NS_Ma) 
ON DELETE CASCADE

ALTER TABLE TaiKhoan 
ADD CONSTRAINT TK_FK1 FOREIGN KEY 
(NS_Ma) REFERENCES NhanSu(NS_Ma) 
ON DELETE CASCADE

ALTER TABLE ChamCong 
ADD CONSTRAINT CC_FK1 FOREIGN KEY 
(NS_Ma) REFERENCES NhanSu(NS_Ma) 
ON DELETE CASCADE

ALTER TABLE dbo.KhenThuongKyluat
ADD CONSTRAINT KTKL_FK1 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)
ON DELETE CASCADE

ALTER TABLE dbo.DangKyCaLam ADD
CONSTRAINT DKCL_FK1 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)
ON DELETE CASCADE,
CONSTRAINT DKCL_KF2 FOREIGN KEY(CL_Ma)
REFERENCES dbo.CaLam(CL_Ma) 
ON DELETE CASCADE,
CONSTRAINT DKCL_FK3 FOREIGN KEY(DDK_Ma)
REFERENCES dbo.DuyetDangKy(DDK_Ma)
ON DELETE CASCADE

ALTER TABLE dbo.DangKyNghiLam ADD 
CONSTRAINT DKNL_FK1 FOREIGN KEY(DDK_Ma)
REFERENCES dbo.DuyetDangKy(DDK_Ma)
ON DELETE CASCADE,
CONSTRAINT DKNL_FK2 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)
ON DELETE CASCADE

ALTER TABLE dbo.HopDong ADD
CONSTRAINT HD_FK11 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)
ON DELETE CASCADE

ALTER TABLE dbo.LichSuLamViec ADD
CONSTRAINT LSLV_FK1 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)
ON DELETE CASCADE,
CONSTRAINT LSLV_FK2 FOREIGN KEY(BP_Ma)
REFERENCES dbo.BoPhan(BP_Ma)
ON DELETE SET NULL

ALTER TABLE dbo.BoPhan ADD
CONSTRAINT BP_FK1 FOREIGN KEY(PB_Ma)
REFERENCES dbo.PhongBan(PB_Ma)
ON DELETE CASCADE,
CONSTRAINT BP_FK2 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)
ON DELETE SET NULL

ALTER TABLE dbo.PhongBan ADD
CONSTRAINT PB_FK11 FOREIGN KEY(NS_Ma)
REFERENCES dbo.NhanSu(NS_Ma)
ON DELETE SET NULL


INSERT INTO dbo.NhanSu
(
    NS_Ma,
    NS_HoVaTen,
    NS_GioiTinh,
    NS_NgaySinh,
    NS_SoDienThoai,
    NS_Email,
    NS_DiaChi,
    NS_SoCCCD,
    NS_SoTaiKhoanNganHang,
    NS_TenChuTaiKhoan,
    NS_HocVan,
    NS_NgayVao
)
VALUES
(   'admin',        -- NS_Ma - char(10)
    N'admin',       -- NS_HoVaTen - nvarchar(50)
    1,      -- NS_GioiTinh - bit
    '2003-11-10', -- NS_NgaySinh - date
    '0886454996',        -- NS_SoDienThoai - char(10)
    'thuong0206066@huce.edu.vn',        -- NS_Email - varchar(100)
    N'Hoàng Văn Thái',       -- NS_DiaChi - ntext
    '036203013524',        -- NS_SoCCCD - char(12)
    NULL,      -- NS_SoTaiKhoanNganHang - char(10)
    NULL,      -- NS_TenChuTaiKhoan - varchar(50)
    NULL,      -- NS_HocVan - nvarchar(20)
    GETDATE()  -- NS_NgayVao - date
    )

INSERT INTO dbo.TaiKhoan
(
    TK_TenDangNhap,
    TK_MatKhau,
    TK_QuyenAdmin,
    TK_QuyenQuanLy,
    TK_BiKhoa,
    TK_ThoiGianMoKhoa,
    TK_MaXacThuc,
    TK_ThoiGianTaoMa,
    TK_AnhDaiDien,
    NS_Ma
)
VALUES
(   'admin',      -- TK_TenDangNhap - varchar(30)
    '$2a$10$WY/kemLQ5LkyQXEiH71pMefNH4UpOuDBBaou9tiPD/Ah24NTgHReK',      -- TK_MatKhau - text
    1, -- TK_QuyenAdmin - bit
    1, -- TK_QuyenQuanLy - bit
    0, -- TK_BiKhoa - bit
    NULL,    -- TK_ThoiGianMoKhoa - datetime
    NULL,    -- TK_MaXacThuc - char(6)
    NULL,    -- TK_ThoiGianTaoMa - datetime
    NULL,    -- TK_AnhDaiDien - text
    'admin'       -- NS_Ma - char(10)
    )

INSERT INTO dbo.BaoHiem
(
    BH_SoBaoHiem,
    BH_NgayCap,
    BH_NgayHetHan,
    BH_NoiCap,
    BH_NoiKhamBenh,
    NS_Ma
)
VALUES
(   '',        -- BH_SoBaoHiem - char(10)
    GETDATE(), -- BH_NgayCap - date
    GETDATE(), -- BH_NgayHetHan - date
    N'',       -- BH_NoiCap - nvarchar(50)
    NULL,      -- BH_NoiKhamBenh - nvarchar(50)
    ''         -- NS_Ma - char(10)
    )

UPDATE dbo.TaiKhoan SET TK_BiKhoa = 0 WHERE TK_TenDangNhap = 'Thuong21'





INSERT INTO NhanSu VALUES ('NS01', N'Nguyễn Tuấn Dũng', 1, '1998-4-2', '0326286549', 'nguyentuandung@gmail.com', N'Thái Bình', '08301867878', '4604335727', 'NGUYEN TUAN DUNG', N'Thạc sĩ', '2023-3-16')
INSERT INTO NhanSu VALUES ('NS02', N'Lê Tùng Lâm', 1, '1999-8-7', '0897287531', 'letunglam@gmail.com', N'Hà Nam', '08803414842', '7601917565', 'LE TUNG LAM', N'Đại học', '2023-1-6')
INSERT INTO NhanSu VALUES ('NS03', N'Hoàng Minh Đức', 1, '2004-8-5', '0354229521', 'hoangminhduc2109@gmail.com', N'Hà Nội', '00104114782', '1701432185', 'HOANG MINH DUC', N'Đại học', '2023-2-26')
INSERT INTO NhanSu VALUES ('NS04', N'Bùi Tuấn Anh', 1, '1996-5-9', '0378745837', 'buituanana0606@gmail.com', N'Quảng Ninh', '01402196196', '2302994596', 'BUI TUAN ANH', N'Đại học', '2022-9-20')
INSERT INTO NhanSu VALUES ('NS05', N'Nguyễn Phương Mai', 0, '2002-6-9', '0978681304', 'nguyenphuongmai2107@gmail.com', N'Hải Dương', '04201468182', '0100123712', 'NGUYEN PHUONG MAI', N'Đại học', '2023-9-21')
INSERT INTO NhanSu VALUES ('NS06', N'Phạm Thị Ngân', 0, '2001-9-7', '0379481118', 'nguyenminhtri22@gmail.com', N'Nam Định', '06105523845', '2504433289', 'PHAM THI NGAN', N'Đại học', '2023-3-16')
INSERT INTO NhanSu VALUES ('NS07', N'Nguyễn Minh Trí', 1, '2002-11-11', '0765649817', 'phamthingan1589@gmail.com', N'Hà Nội', '05905725797', '5105532575', 'NGUYEN MINH TRI', N'Đại học', '2023-1-6')
INSERT INTO NhanSu VALUES ('NS08', N'Đỗ Thị Thoa', 0, '2001-5-1', '0974387516', 'thoa215896@gmail.com', N'Bắc Ninh', '06205736357', '5301396955', 'DO THI THOA', N'Cấp 3', '2023-2-26')
INSERT INTO NhanSu VALUES ('NS09', N'Nguyễn Minh Hiếu', 1, '2003-2-7', '0978681304', 'hieunguyen5874@gmail.com', N'BắcGiang', '02402977658', '8605777772', 'NGUYEN MINH HIEU', N'Đại học', '2022-9-20')
INSERT INTO NhanSu VALUES ('NS10', N'Võ Đình Thủy', 0, '2001-11-30', '0867920231', 'thuydinhvo@gmail.com', N'Cao Bằng', '07201696955', '3004992861', 'VO DINH THUY', N'Đại học', '2023-9-21')
INSERT INTO NhanSu VALUES ('NS11', N'Nguyễn Hà My', 1, '1998-12-14', '0972138977', 'my5842@gmail.com', N'Nam Định', '04405232796', '5701397571', 'NGUYEN HA MY', N'Đại học', '2023-3-16')
INSERT INTO NhanSu VALUES ('NS12', N'Nguyễn Hùng Anh', 1, '1997-5-21', '0835206314', 'hunganhyeuemnhat@gmail.com', N'Hà Nội', '00400655192', '7705712445', 'NGUYEN HUNG ANH', N'Đại học', '2023-1-6')
INSERT INTO NhanSu VALUES ('NS13', N'Đỗ Ngọc Cường', 1, '1998-4-7', '0978529309', 'dongoccuong@gmail.com', N'Lai Châu', '05801633644', '8202256714', 'DO NGOC CUONG', N'Đại học', '2023-2-26')
INSERT INTO NhanSu VALUES ('NS14', N'Mạc Văn Hiếu', 1, '2000-3-5', '0981026119', 'hieumacvan1524@gmail.com', N'Nam Định', '02200558414', '7004872529', 'MAC VAN HIEU', N'Thạc sĩ', '2022-9-20')
INSERT INTO NhanSu VALUES ('NS15', N'Trần Quỳnh Phương', 0, '2000-6-8', '0328889440', 'phuong125478@gmail.com', N'Bắc Ninh', '07000481564', '2501167576', 'TRAN QUYNH PHUONG', N'Đại học', '2023-9-21')
INSERT INTO NhanSu VALUES ('NS16', N'Ngô Văn Luân', 1, '1998-8-7', '0969446106', 'lamsenpai987@gmail.com', N'Bắc Giang', '01105545483', '2605971389', 'NGO VAN LUAN', N'Đại học', '2023-3-16')
INSERT INTO NhanSu VALUES ('NS17', N'Nguyễn Thị Ánh Tuyết', 0, '1999-2-8', '0352235726', 'tuyet2587@gmail.com', N'Sơn Lai', '04405376278', '9303116561', 'NGUYEN THI ANH TUYET', N'Đại học', '2023-1-6')
INSERT INTO NhanSu VALUES ('NS18', N'Nguyễn Vĩnh Tứ', 1, '2001-5-14', '0389019292', 'tutuyetgiangho@gmail.com', N'Cao Bằng', '03905196152', '6903436461', 'NGUYEN VINH TU', N'Đại học', '2023-2-26')
INSERT INTO NhanSu VALUES ('NS19', N'Phạm Quốc An', 1, '2002-9-14', '0869261276', 'anbao12547@gmail.com', N'Tây Nguyên', '08103282173', '0800796437', 'PHAM QUOC AN', N'Cấp 3', '2022-9-20')
INSERT INTO NhanSu VALUES ('NS20', N'Hoàng Bá Anh', 1, '2003-7-31', '0914440766', 'anhhoang57852@gmail.com', N'Hà Nội', '09102278767', '6201472289', 'HOANG BA OANH', N'Đại học', '2023-8-12')



INSERT INTO BaoHiem VALUES ('3110186549', '2022-9-12', '2024-9-12', N'Trường ĐHXD', N'BV Thanh Nhàn', 'NS01')
INSERT INTO BaoHiem VALUES ('2720575311', '2022-9-13', '2024-9-13', N'Trường ĐHXD', N'BV Thanh Nhàn', 'NS02')
INSERT INTO BaoHiem VALUES ('2150552114', '2022-9-14', '2024-9-14', N'Trường ĐHXD', N'BV Thanh Nhàn', 'NS03')
INSERT INTO BaoHiem VALUES ('2550326541', '2022-9-15', '2024-9-15', N'Trường ĐHXD', N'BV Thanh Nhàn', 'NS04')
INSERT INTO BaoHiem VALUES ('2760338799', '2022-9-16', '2024-9-16', N'Trường ĐHXD', N'BV Thanh Nhàn', 'NS05')
INSERT INTO BaoHiem VALUES ('2140182889', '2022-9-17', '2024-9-17', N'Trường ĐHXD', N'BV Bạch Mai', 'NS06')
INSERT INTO BaoHiem VALUES ('2590138141', '2022-9-18', '2024-9-18', N'Trường ĐHXD', N'BV Bạch Mai', 'NS07')
INSERT INTO BaoHiem VALUES ('3160533678', '2022-9-19', '2024-9-19', N'Trường ĐHXD', N'BV Bạch Mai', 'NS08')
INSERT INTO BaoHiem VALUES ('2470455971', '2022-9-20', '2024-9-20', N'Trường ĐHXD', N'BV Bạch Mai', 'NS09')
INSERT INTO BaoHiem VALUES ('2370364234', '2022-9-21', '2024-9-21', N'Trường ĐHXD', N'BV Bạch Mai', 'NS10')
INSERT INTO BaoHiem VALUES ('2500442651', '2022-9-22', '2024-9-22', N'Trường ĐHXD', N'BV Bạch Mai', 'NS11')
INSERT INTO BaoHiem VALUES ('2490529734', '2022-9-23', '2024-9-23', N'Trường ĐHXD', N'BV Bạch Mai', 'NS12')
INSERT INTO BaoHiem VALUES ('2930088622', '2022-9-24', '2024-9-24', N'Trường ĐHXD', N'BV Bạch Mai', 'NS13')
INSERT INTO BaoHiem VALUES ('2460374367', '2022-9-25', '2024-9-25', N'Trường ĐHXD', N'BV Trung Ương', 'NS14')
INSERT INTO BaoHiem VALUES ('2700048156', '2022-9-26', '2024-9-26', N'Trường ĐHXD', N'BV Trung Ương', 'NS15')
INSERT INTO BaoHiem VALUES ('2110554548', '2022-9-27', '2024-9-27', N'Trường ĐHXD', N'BV Trung Ương', 'NS16')
INSERT INTO BaoHiem VALUES ('2440537627', '2022-9-28', '2024-9-28', N'Trường ĐHXD', N'BV Trung Ương', 'NS17')
INSERT INTO BaoHiem VALUES ('2390519615', '2022-9-29', '2024-9-29', N'Trường ĐHXD', N'BV Trung Ương', 'NS18')
INSERT INTO BaoHiem VALUES ('2810328217', '2022-9-30', '2024-9-30', N'Trường ĐHXD', N'BV Trung Ương', 'NS19')
INSERT INTO BaoHiem VALUES ('2910227876', '2022-10-1', '2024-10-1', N'Trường ĐHXD', N'BV Trung Ương', 'NS20')

INSERT INTO dbo.HopDong
(
    HD_NgayBatDau,
    HD_NgayKetThuc,
    HD_HinhThucLamViec,
    HD_Luong,
    HD_DonViTinhuong,
    HD_CongViec,
    NS_Ma
)
VALUES
(   GETDATE(), -- HD_NgayBatDau - date
    NULL,      -- HD_NgayKetThuc - date
    N'',       -- HD_HinhThucLamViec - nvarchar(30)
    0.0,       -- HD_Luong - float
    N'',       -- HD_DonViTinhuong - nvarchar(20)
    N'',       -- HD_CongViec - nvarchar(30)
    ''         -- NS_Ma - char(10)
    )




INSERT INTO HopDong VALUES ('2022-6-2', '2023-6-2', N'PartTime', 24000, N'Giờ', N'Gõ văn bản', 'NS09')
INSERT INTO HopDong VALUES ('2022-6-2', '2023-6-2', N'FullTime', 5000000, N'Tháng', N'Quản trị hệ thống ', 'NS06')
INSERT INTO HopDong VALUES ('2022-6-3', '2023-6-3', N'FullTime', 5000000, N'Tháng', N'Quản trị hệ thống ', 'NS01')
INSERT INTO HopDong VALUES ('2022-6-4', '2023-6-4', N'FullTime', 5000000, N'Tháng', N'Quản trị hệ thống ', 'NS02')
INSERT INTO HopDong VALUES ('2022-6-5', '2023-6-5', N'PartTime', 24000, N'Giờ', N'Gõ văn bản', 'NS03')
INSERT INTO HopDong VALUES ('2022-6-6', '2023-6-6', N'PartTime', 24000, N'Giờ', N'Gõ văn bản', 'NS04')



INSERT INTO dbo.CaLam
(
    CL_Ma,
    CL_TenCa,
    CL_GioBatDau,
    CL_PhutBatDau,
    CL_GioKetThuc,
    CL_PhutKetThuc
)
VALUES
(   '',   -- CL_Ma - char(10)
    NULL, -- CL_TenCa - nvarchar(30)
    NULL, -- CL_GioBatDau - smallint
    NULL, -- CL_PhutBatDau - smallint
    NULL, -- CL_GioKetThuc - smallint
    NULL  -- CL_PhutKetThuc - smallint
    )

INSERT INTO CaLam VALUES ('CL01', N'Ca sáng', 7, 0, 11, 0)
INSERT INTO CaLam VALUES ('CL02', N'Ca chiều', 13, 0, 17, 0)
INSERT INTO CaLam VALUES ('CL03', N'Ca 1', 7, 0, 9, 0)
INSERT INTO CaLam VALUES ('CL04', N'Ca 2', 9, 0, 11, 0)
INSERT INTO CaLam VALUES ('CL05', N'Ca 3', 13, 0, 15, 0)
INSERT INTO CaLam VALUES ('CL06', N'Ca 4', 15, 0, 17, 0)


INSERT INTO dbo.LichSuLamViec
(
    LSLV_NgayBatDau,
    LSLV_NgayKetThuc,
    LSLV_ChucVu,
    NS_Ma,
    BP_Ma
)
VALUES
(   GETDATE(), -- LSLV_NgayBatDau - date
    NULL,      -- LSLV_NgayKetThuc - date
    N'',       -- LSLV_ChucVu - nvarchar(50)
    '',        -- NS_Ma - char(10)
    NULL       -- BP_Ma - char(10)
    )

SELECT * FROM dbo.PhongBan

INSERT INTO dbo.PhongBan
(
    PB_Ma,
    PB_Ten,
    PB_VaiTro,
    NS_Ma
)
VALUES
(   '',   -- PB_Ma - char(10)
    N'',  -- PB_Ten - nvarchar(50)
    NULL, -- PB_VaiTro - nvarchar(50)
    NULL  -- NS_Ma - char(10)
    )

INSERT INTO dbo.BoPhan
(
    BP_Ma,
    BP_Ten,
    BP_ChuyenMon,
    PB_Ma,
    NS_Ma
)
VALUES
(   '',   -- BP_Ma - char(10)
    N'',  -- BP_Ten - nvarchar(50)
    NULL, -- BP_ChuyenMon - nvarchar(50)
    '',   -- PB_Ma - char(10)
    NULL  -- NS_Ma - char(10)
    )