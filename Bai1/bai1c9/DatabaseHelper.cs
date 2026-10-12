using System;
using Microsoft.Data.SqlClient;

namespace bai1c9;

public static class DatabaseHelper
{
    private static string? _workingConnectionString;

    public static string GetConnectionString()
    {
        if (_workingConnectionString != null)
            return _workingConnectionString;

        // Danh sách các instance SQL Server phổ biến trên máy tính
        string[] candidates = new[]
        {
            @"(localdb)\MSSQLLocalDB",
            @".\SQLEXPRESS",
            @"(local)\SQLEXPRESS",
            @".",
            @"localhost"
        };

        foreach (var server in candidates)
        {
            string masterConnStr = $"Server={server};Database=master;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=3;";
            try
            {
                using var conn = new SqlConnection(masterConnStr);
                conn.Open();

                // Đã tìm thấy SQL Server hoạt động -> Đảm bảo database và bảng tồn tại
                EnsureDatabaseAndSeed(conn, server);

                _workingConnectionString = $"Server={server};Database=TiemTapHoa;Trusted_Connection=True;TrustServerCertificate=True;";
                return _workingConnectionString;
            }
            catch
            {
                // Thử server tiếp theo
            }
        }

        // Mặc định trả về LocalDB theo đề bài
        _workingConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=TiemTapHoa;Trusted_Connection=True;TrustServerCertificate=True;";
        return _workingConnectionString;
    }

    private static void EnsureDatabaseAndSeed(SqlConnection masterConn, string server)
    {
        try
        {
            // 1. Kiểm tra database TiemTapHoa
            bool dbExists = false;
            using (var cmd = new SqlCommand("SELECT database_id FROM sys.databases WHERE name = 'TiemTapHoa'", masterConn))
            {
                var res = cmd.ExecuteScalar();
                dbExists = (res != null && res != DBNull.Value);
            }

            if (!dbExists)
            {
                using var cmdCreate = new SqlCommand("CREATE DATABASE TiemTapHoa", masterConn);
                cmdCreate.ExecuteNonQuery();
            }

            // 2. Kết nối vào TiemTapHoa để kiểm tra bảng HangHoa
            string appConnStr = $"Server={server};Database=TiemTapHoa;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=5;";
            using var appConn = new SqlConnection(appConnStr);
            appConn.Open();

            bool tableExists = false;
            using (var cmd = new SqlCommand("SELECT object_id FROM sys.tables WHERE name = 'HangHoa'", appConn))
            {
                var res = cmd.ExecuteScalar();
                tableExists = (res != null && res != DBNull.Value);
            }

            if (!tableExists)
            {
                string initSql = @"
CREATE TABLE HangHoa (
    MaHang INT IDENTITY(1,1) PRIMARY KEY,
    TenHang NVARCHAR(100) NOT NULL,
    LoaiHang NVARCHAR(50),
    DonVi NVARCHAR(20),
    GiaBan DECIMAL(18,0) CHECK (GiaBan > 0),
    SoLuongTon INT CHECK (SoLuongTon >= 0),
    NgayNhap DATETIME,
    ConKinhDoanh BIT NOT NULL DEFAULT 1
);

INSERT INTO HangHoa (TenHang, LoaiHang, DonVi, GiaBan, SoLuongTon, NgayNhap, ConKinhDoanh) VALUES
(N'Nước suối Aquafina 500ml', N'Đồ uống', N'Chai', 6000, 120, '2026-10-02 00:00:00', 1),
(N'Mì Hảo Hảo tôm chua cay', N'Thực phẩm khô', N'Gói', 4500, 200, '2026-10-02 00:00:00', 1),
(N'Dầu ăn Tường An 1L', N'Gia vị', N'Chai', 48000, 35, '2026-10-05 00:00:00', 1),
(N'Bột giặt OMO 800g', N'Đồ gia dụng', N'Túi', 52000, 40, '2026-10-05 00:00:00', 1),
(N'Sữa tươi Vinamilk 1L', N'Đồ uống', N'Hộp', 32000, 60, '2026-10-08 00:00:00', 1),
(N'Nước mắm Nam Ngư 500ml', N'Gia vị', N'Chai', 38000, 25, '2026-10-01 00:00:00', 0);
";
                using var cmdInit = new SqlCommand(initSql, appConn);
                cmdInit.ExecuteNonQuery();
            }
        }
        catch
        {
            // Bỏ qua lỗi nếu đã được tạo trước đó
        }
    }
}
