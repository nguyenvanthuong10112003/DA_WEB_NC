DROP TRIGGER dbo.TRIGGER_NHANSU

CREATE TRIGGER TRIGGER_NHANSU 
ON dbo.NhanSu
FOR DELETE
AS
BEGIN
		PRINT('OK')
		DECLARE @id CHAR(10)
		SET @id = (SELECT NS_Ma FROM dbo.NhanSu INNER JOIN Deleted ON Deleted.NS_Ma = NhanSu.NS_Ma)
		DELETE dbo.TaiKhoan WHERE NS_Ma = @id
		DELETE dbo.BaoHiem WHERE NS_Ma = @id
		DELETE dbo.ChamCong WHERE NS_Ma = @id
		DELETE dbo.DangKyCaLam WHERE NS_Ma = @id
		DELETE dbo.DangKyNghiLam WHERE NS_Ma = @id
		DELETE dbo.HopDong WHERE NS_Ma = @id
		DELETE dbo.LichSuLamViec WHERE NS_Ma = @id

		UPDATE dbo.PhongBan SET NS_Ma = NULL WHERE NS_Ma = @id
		UPDATE dbo.BoPhan SET NS_Ma = NULL WHERE NS_Ma = @id
		UPDATE dbo.DuyetDangKy SET NS_Ma = NULL WHERE NS_Ma = @id
		UPDATE dbo.LichSuHanhDong SET NS_Ma = NULL WHERE NS_Ma = @id
		UPDATE dbo.KhenThuongKyluat SET NS_Ma = NULL WHERE NS_Ma = @id
END

DELETE dbo.NhanSu WHERE NS_Ma = 'TV001'


ALTER TABLE dbo.TaiKhoan 
DROP TK_FK1

ALTER TABLE dbo.TaiKhoan ADD CONSTRAINT TK_FK1 FOREIGN KEY (NS_Ma) REFERENCES dbo.NhanSu(NS_Ma) ON DELETE CASCADE

CREATE PROC SORT @sortBy nvarchar(20)
AS
BEGIN
	SELECT * FROM dbo.NhanSu
	ORDER BY @sortBy
END

SELECT * FROM dbo.NhanSu

SELECT * FROM dbo.TaiKhoan



GO
CREATE TRIGGER TRIGGER_DuyetDangKy
ON dbo.DuyetDangKy
FOR DELETE
AS 
BEGIN
	UPDATE dbo.DangKyCaLam SET DKCL_DaDuocDuyet = 0 
	WHERE DDK_Ma IN (SELECT d.DDK_Ma FROM Deleted d)
	UPDATE dbo.DangKyNghiLam SET DKNL_DaDuocDuyet = 0 
	WHERE DDK_Ma IN (SELECT d.DDK_Ma FROM Deleted d)
END

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
(   '',        -- NS_Ma - char(10)
    N'',       -- NS_HoVaTen - nvarchar(50)
    NULL,      -- NS_GioiTinh - bit
    GETDATE(), -- NS_NgaySinh - date
    '',        -- NS_SoDienThoai - char(10)
    '',        -- NS_Email - varchar(100)
    N'',       -- NS_DiaChi - ntext
    '',        -- NS_SoCCCD - char(12)
    NULL,      -- NS_SoTaiKhoanNganHang - char(10)
    NULL,      -- NS_TenChuTaiKhoan - varchar(50)
    NULL,      -- NS_HocVan - nvarchar(20)
    GETDATE()  -- NS_NgayVao - date
    )
INSERT INTO dbo.BaoHiem
(
    BH_Ma,
    BH_SoBaoHiem,
    BH_NgayCap,
    BH_NgayHetHan,
    BH_NoiCap,
    BH_NoiKhamBenh,
    NS_Ma
)
VALUES
(   '',        -- BH_Ma - char(10)
    '',        -- BH_SoBaoHiem - char(10)
    GETDATE(), -- BH_NgayCap - date
    GETDATE(), -- BH_NgayHetHan - date
    N'',       -- BH_NoiCap - nvarchar(50)
    NULL,      -- BH_NoiKhamBenh - nvarchar(50)
    ''         -- NS_Ma - char(10)
    )


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




INSERT INTO BaoHiem VALUES ('BH001', '3110186549', '2022-9-12', '2024-9-12', N'Trường ĐHXD', N'BV Thanh Nhàn', 'NS01')
INSERT INTO BaoHiem VALUES ('BH002', '2720575311', '2022-9-13', '2024-9-13', N'Trường ĐHXD', N'BV Thanh Nhàn', 'NS02')
INSERT INTO BaoHiem VALUES ('BH003', '2150552114', '2022-9-14', '2024-9-14', N'Trường ĐHXD', N'BV Thanh Nhàn', 'NS03')
INSERT INTO BaoHiem VALUES ('BH004', '2550326541', '2022-9-15', '2024-9-15', N'Trường ĐHXD', N'BV Thanh Nhàn', 'NS04')
INSERT INTO BaoHiem VALUES ('BH005', '2760338799', '2022-9-16', '2024-9-16', N'Trường ĐHXD', N'BV Thanh Nhàn', 'NS05')
INSERT INTO BaoHiem VALUES ('BH006', '2140182889', '2022-9-17', '2024-9-17', N'Trường ĐHXD', N'BV Bạch Mai', 'NS06')
INSERT INTO BaoHiem VALUES ('BH007', '2590138141', '2022-9-18', '2024-9-18', N'Trường ĐHXD', N'BV Bạch Mai', 'NS07')
INSERT INTO BaoHiem VALUES ('BH008', '3160533678', '2022-9-19', '2024-9-19', N'Trường ĐHXD', N'BV Bạch Mai', 'NS08')
INSERT INTO BaoHiem VALUES ('BH009', '2470455971', '2022-9-20', '2024-9-20', N'Trường ĐHXD', N'BV Bạch Mai', 'NS09')
INSERT INTO BaoHiem VALUES ('BH010', '2370364234', '2022-9-21', '2024-9-21', N'Trường ĐHXD', N'BV Bạch Mai', 'NS10')
INSERT INTO BaoHiem VALUES ('BH011', '2500442651', '2022-9-22', '2024-9-22', N'Trường ĐHXD', N'BV Bạch Mai', 'NS11')
INSERT INTO BaoHiem VALUES ('BH012', '2490529734', '2022-9-23', '2024-9-23', N'Trường ĐHXD', N'BV Bạch Mai', 'NS12')
INSERT INTO BaoHiem VALUES ('BH013', '2930088622', '2022-9-24', '2024-9-24', N'Trường ĐHXD', N'BV Bạch Mai', 'NS13')
INSERT INTO BaoHiem VALUES ('BH014', '2460374367', '2022-9-25', '2024-9-25', N'Trường ĐHXD', N'BV Trung Ương', 'NS14')
INSERT INTO BaoHiem VALUES ('BH015', '2700048156', '2022-9-26', '2024-9-26', N'Trường ĐHXD', N'BV Trung Ương', 'NS15')
INSERT INTO BaoHiem VALUES ('BH016', '2110554548', '2022-9-27', '2024-9-27', N'Trường ĐHXD', N'BV Trung Ương', 'NS16')
INSERT INTO BaoHiem VALUES ('BH017', '2440537627', '2022-9-28', '2024-9-28', N'Trường ĐHXD', N'BV Trung Ương', 'NS17')
INSERT INTO BaoHiem VALUES ('BH018', '2390519615', '2022-9-29', '2024-9-29', N'Trường ĐHXD', N'BV Trung Ương', 'NS18')
INSERT INTO BaoHiem VALUES ('BH019', '2810328217', '2022-9-30', '2024-9-30', N'Trường ĐHXD', N'BV Trung Ương', 'NS19')
INSERT INTO BaoHiem VALUES ('BH020', '2910227876', '2022-10-1', '2024-10-1', N'Trường ĐHXD', N'BV Trung Ương', 'NS20')
