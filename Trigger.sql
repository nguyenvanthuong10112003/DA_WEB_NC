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