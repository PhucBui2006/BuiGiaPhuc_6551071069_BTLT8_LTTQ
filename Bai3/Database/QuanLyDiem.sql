/* =========================================================
   BAI TAP CHUONG 9 - CAU 3 : PHIEU BAO DIEM CUA KHOA
   CSDL : QuanLyDiem  (SQL Server LocalDB)
   Chay script nay trong SSMS (mo file -> F5)
   hoac : sqlcmd -S "(localdb)\MSSQLLocalDB" -i QuanLyDiem.sql -f 65001
   ========================================================= */

USE master;
GO

IF DB_ID(N'QuanLyDiem') IS NOT NULL
BEGIN
    ALTER DATABASE QuanLyDiem SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyDiem;
END
GO

CREATE DATABASE QuanLyDiem;
GO

USE QuanLyDiem;
GO

/* ---------------- Bang Lop ---------------- */
CREATE TABLE dbo.Lop
(
    MaLop  varchar(10)   NOT NULL,
    TenLop nvarchar(100) NOT NULL,
    CONSTRAINT PK_Lop PRIMARY KEY (MaLop)
);
GO

/* ---------------- Bang SinhVien ---------------- */
CREATE TABLE dbo.SinhVien
(
    MaSV     varchar(10)   NOT NULL,
    HoTen    nvarchar(100) NOT NULL,
    NgaySinh datetime      NOT NULL,
    GioiTinh nvarchar(3)   NOT NULL,
    MaLop    varchar(10)   NOT NULL,
    CONSTRAINT PK_SinhVien PRIMARY KEY (MaSV),
    CONSTRAINT FK_SinhVien_Lop FOREIGN KEY (MaLop) REFERENCES dbo.Lop (MaLop)
);
GO

/* ---------------- Bang DiemMon ---------------- */
CREATE TABLE dbo.DiemMon
(
    MaDiem     int IDENTITY(1,1) NOT NULL,
    MaSV        varchar(10)   NOT NULL,
    TenMon      nvarchar(100) NOT NULL,
    SoTinChi    int           NOT NULL,
    Diem        decimal(4,2)  NOT NULL,
    CONSTRAINT PK_DiemMon PRIMARY KEY (MaDiem),
    CONSTRAINT FK_DiemMon_SinhVien FOREIGN KEY (MaSV) REFERENCES dbo.SinhVien (MaSV),
    CONSTRAINT CK_DiemMon_SoTinChi CHECK (SoTinChi BETWEEN 1 AND 6),
    CONSTRAINT CK_DiemMon_Diem     CHECK (Diem BETWEEN 0 AND 10)
);
GO

/* ---------------- Du lieu mau ---------------- */
INSERT INTO dbo.Lop (MaLop, TenLop) VALUES
    ('CNTT01', N'Công nghệ thông tin K01'),
    ('KT01',   N'Kế toán K01');

INSERT INTO dbo.SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, MaLop) VALUES
    ('SV001', N'Nguyễn Minh Anh', '2005-03-15', N'Nữ',  'CNTT01'),
    ('SV002', N'Trần Quốc Bảo',  '2005-07-22', N'Nam', 'CNTT01'),
    ('SV003', N'Lê Thị Cẩm Tú',   '2005-11-09', N'Nữ',  'KT01');

-- SV001 : 4 mon, tong 11 TC, DTB 7.77 -> Kha
INSERT INTO dbo.DiemMon (MaSV, TenMon, SoTinChi, Diem) VALUES
    ('SV001', N'Lập trình trực quan',      3, 8.50),
    ('SV001', N'Cơ sở dữ liệu',            3, 7.50),
    ('SV001', N'Tiếng Anh chuyên ngành',   2, 9.00),
    ('SV001', N'Toán rời rạc',             3, 6.50);

-- SV002 : 3 mon, tong 8 TC, DTB 8.50 -> Gioi
INSERT INTO dbo.DiemMon (MaSV, TenMon, SoTinChi, Diem) VALUES
    ('SV002', N'Lập trình trực quan',      3, 9.00),
    ('SV002', N'Cơ sở dữ liệu',            3, 8.00),
    ('SV002', N'Tiếng Anh chuyên ngành',   2, 8.50);

-- SV003 : chua co diem mon nao
GO

/* ---------------- Kiem tra nhanh ---------------- */
SELECT * FROM dbo.Lop;
SELECT * FROM dbo.SinhVien;
SELECT * FROM dbo.DiemMon;
GO
